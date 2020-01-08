using System;

namespace CodiceFiscaleUI.Settings
{
    public class AppSetting<T>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public T Value { get; set; }

        public Type GetInnerType()
        {
            return Value.GetType();
        }
    }
}