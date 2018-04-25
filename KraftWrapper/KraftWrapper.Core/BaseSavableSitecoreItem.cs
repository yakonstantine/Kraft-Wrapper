using KraftWrapper.Interfaces;
using KraftWrapper.Options;
using System;

namespace KraftWrapper.Core
{
    public abstract class BaseSavableSitecoreItem : ISavableSitecoreItem
    {
        protected readonly ISitecoreConfiguration _sitecoreConfiguration;
        protected readonly ISitecoreFactory _sitecoreFactory;
        protected readonly ISitecorePublisher _sitecorePublisher;

        private ISitecoreDatabase _masterDb;

        protected BaseSavableSitecoreItem(
            ISitecoreFactory sitecoreFactory,
            ISitecoreConfiguration sitecoreConfiguration,
            ISitecorePublisher sitecorePublisher)
        {
            _sitecoreFactory = sitecoreFactory;
            _sitecoreConfiguration = sitecoreConfiguration;
            _sitecorePublisher = sitecorePublisher;
        }

        public abstract ISitecoreItem SourceItem { get; protected set; }
        public abstract ISitecoreLanguage Language { get; protected set; }
        protected ISitecoreDatabase MasterDb
        {
            get
            {
                if (_masterDb == null)
                {
                    _masterDb = _sitecoreFactory.GetDatabase("master");
                }

                return _masterDb;
            }
        }

        public void Save()
        {
            if (this.Language == null)
            {
                throw new NullReferenceException("The language is not initialized.");
            }

            _sitecoreConfiguration.DisableSecurity();
            _sitecoreConfiguration.SwitchLanguage(this.Language);

            if (this.SourceItem == null)
            {
                CreateNewSourceItem();
            }

            this.SourceItem.BeginEdit();

            try
            {
                SetSourceItemFieldValues();

                this.SourceItem.EndEdit();
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error($"Could not update item {this.SourceItem.FullPath}: {ex.Message}", this);

                this.SourceItem.CancelEdit();

                throw;
            }
            finally
            {
                _sitecoreConfiguration.EnableSecurity();
                _sitecoreConfiguration.ResetLanguage();
            }
        }

        public void Publish(PublishMode publishMode, bool deep, bool withRelatedItems)
        {
            if (this.SourceItem == null)
            {
                throw new NullReferenceException("The target sitecore item doesn't created or set");
            }

            if (this.Language == null)
            {
                throw new NullReferenceException("The language is not initialized.");
            }

            try
            {
                var webDb = _sitecoreFactory.GetDatabase("web");

                var publishOptions = new PublishOptions(this.MasterDb, webDb, this.SourceItem, this.Language)
                {
                    Mode = publishMode,
                    Deep = deep,
                    PublishRelatedItems = withRelatedItems
                };

                _sitecorePublisher.Publish(publishOptions);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Exception publishing items from custom pipeline! : " + ex, this);
                throw;
            }
        }

        protected abstract void CreateNewSourceItem();

        protected abstract void SetSourceItemFieldValues();
    }
}
