﻿using KraftWrapper.Interfaces;
using Sitecore.Globalization;
using System;
using System.Globalization;

namespace KraftWrapper.Core
{
    public class SitecoreLanguage : ISitecoreLanguage
    {
        private readonly Language _language;

        public SitecoreLanguage(Language language)
        {
            _language = language ?? throw new ArgumentNullException("Input language is null."); ;
        }

        public CultureInfo CultureInfo
        {
            get
            {
                return _language.CultureInfo;
            }
        }

        public string Name
        {
            get
            {
                return _language.Name;
            }
        }

        internal Language RawValue
        {
            get
            {
                return _language;
            }
        }
    }
}
