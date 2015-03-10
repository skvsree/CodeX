using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using CodeX;

// ReSharper disable CheckNamespace
namespace CodeX.Windows.Forms
// ReSharper restore CheckNamespace
{
    using CodeX.Core;

    public class ThemeListConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("themes")]
        public ThemeCollection Themes
        {
            get { return (ThemeCollection)this["themes"]; }
        }
    }

    public class ThemeCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ThemeConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ThemeConfiguration)element).Name;
        }

        public void Remove(ThemeConfiguration configuration)
        {
            BaseRemove(configuration.Name);
        }
        
        public void Clear()
        {
            BaseClear();
        }

        public void Add(ThemeConfiguration themeConfiguration)
        {
            LockItem = false;
            BaseAdd(themeConfiguration);
        }
    }

    public class ThemeConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        //[ConfigurationProperty("title", IsRequired = true)]
        //public string Title
        //{
        //    get { return (string)this["title"]; }
        //    set { this["title"] = value; }
        //}

        [ConfigurationProperty("selected", DefaultValue = "true", IsRequired = true)]
        public Boolean Selected
        {
            get { return (Boolean)this["selected"]; }
            set { this["selected"] = value; }
        }

        [ConfigurationProperty("window")]
        public WindowElement Window
        {
            get { return (WindowElement)this["window"]; }
            set { this["window"] = value; }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }

    public class WindowElement : ConfigurationElement
    {
        [ConfigurationProperty("forecolor", DefaultValue = "BlueX")]
        public string ForeColorString
        {
            get { return (string)this["forecolor"]; }
            private set { this["forecolor"] = value; }
        }

        public Color ForeColor
        {
            get { return ThemeColor.GetColor(ForeColorString); }
            set { ForeColorString = ThemeColor.GetColorString(value); }
        }

        [ConfigurationProperty("backcolor", DefaultValue = "DarkGrayX")]
        public string BackColorString
        {
            get { return (string)this["backcolor"]; }
            private set { this["backcolor"] = value; }
        }

        public Color BackColor
        {
            get { return ThemeColor.GetColor(BackColorString); }
            set { BackColorString = ThemeColor.GetColorString(value); }
        }

        [ConfigurationProperty("bordercolor", DefaultValue = "BlueX")]
        public string BorderColorString
        {
            get { return (string)this["bordercolor"]; }
            private set { this["bordercolor"] = value; }
        }

        public Color BorderColor
        {
            get { return ThemeColor.GetColor(BorderColorString); }
            set { BorderColorString = ThemeColor.GetColorString(value); }
        }

        [ConfigurationProperty("animation", DefaultValue = "None")]
        private string AnimationType
        {
            get { return (string)this["animation"]; }
            set { this["animation"] = value; }
        }

        public FormAnimation Animation
        {
            get
            {
                return AnimationType.ToEnum<FormAnimation>();
            }
            set { AnimationType = value.AsString(); }
        }

        [ConfigurationProperty("animationspeed", DefaultValue = "100")]
        public int AnimationSpeed
        {
            get { return (int)this["animationspeed"]; }
            set { this["animationspeed"] = value; }
        }
    }
}