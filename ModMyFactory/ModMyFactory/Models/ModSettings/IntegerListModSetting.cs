﻿using ModMyFactory.ModSettings;
using System.Collections.Generic;
using System.Windows;

namespace ModMyFactory.Models.ModSettings
{
    sealed class IntegerListModSetting : ListModSetting<long>
    {
        sealed class IntegerListModSettingProxy : ListModSettingProxy<long>
        {
            public IntegerListModSettingProxy(IntegerListModSetting baseSetting)
                : base(baseSetting)
            { }

            private IntegerListModSettingProxy(IntegerListModSettingProxy baseSetting)
                : base(baseSetting)
            { }

            public override IModSettingProxy CreateProxy()
            {
                return new IntegerListModSettingProxy(this);
            }
        }

        public override DataTemplate Template => (DataTemplate)App.Instance.Resources["IntegerListModSettingTemplate"];

        public IntegerListModSetting(string name, LoadTime loadTime, string ordering, long defaultValue, IEnumerable<long> allowedValues)
            : base(name, loadTime, ordering, defaultValue, allowedValues)
        { }

        public override IModSettingProxy CreateProxy()
        {
            return new IntegerListModSettingProxy(this);
        }
    }
}
