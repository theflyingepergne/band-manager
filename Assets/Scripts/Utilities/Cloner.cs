using System;
using System.Reflection;

public static class Cloner
{
    public static T CloneFields<T>(T original) where T : class
    {
        if (original == null) return null;

        // Grabs the exact concrete child type at runtime, bypassing the abstract constraint
        Type type = original.GetType();

        // Dynamically creates a fresh instance of that exact child class
        T clone = (T)Activator.CreateInstance(type);

        // Automatically loops through and copies every single private and public field
        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            field.SetValue(clone, field.GetValue(original));
        }

        return clone;
    }
}