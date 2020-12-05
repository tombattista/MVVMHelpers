using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MVVMHelpers.ViewModels.Shared
{
    /// <summary>
    /// ObservableObject is used as a base class to any class which needs to be bound to the UI
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Notifies that a property of a derived object has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies that a collection of a derived object has changed
        /// </summary>
        public event CollectionChangeEventHandler CollectionChanged;
        public event CollectionChangeEventHandler CollectionItemAdded;
        public event CollectionChangeEventHandler CollectionItemRemoved;

        /// <summary>
        /// Notifies that an error exists
        /// </summary>
        public event PropertyChangedEventHandler ErrorExists;

        /// <summary>
        /// Allows a derived object to notify a parent or sibling that its state has changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Allows a derived object to notify a parent or sibling that its state has changed
        /// </summary>
        /// <param name="collectionName"></param>
        protected void RaiseCollectionChangedEvent(string collectionName)
        {
            CollectionChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, collectionName));
        }

        /// <summary>
        /// Allows a derived object to notify a parent or sibling that an item has been added to the collection
        /// </summary>
        /// <param name="collectionName"></param>
        protected void RaiseCollectionItemAddedEvent(string collectionName)
        {
            CollectionItemAdded?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, collectionName));
        }

        /// <summary>
        /// Allows a derived object to notify a parent or sibling that an item has been removed to the collection
        /// </summary>
        /// <param name="collectionName"></param>
        protected void RaiseCollectionItemRemovedEvent(string collectionName)
        {
            CollectionItemRemoved?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, collectionName));
        }

        /// <summary>
        /// Allows a derived object to notify a parent or sibling that an error exists
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaiseErrorExistsEvent(string propertyName)
        {
            ErrorExists?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Use of the id is optional.
        /// Its purpose is to facilitate distinguishing between objects with as little data transferrence as possible.
        /// </summary>
        public Guid Id { get { return id; } }
        protected Guid id;

        /// <summary>
        /// Used to describe the full object type.  Example: "RootType.SubType.ObjectType"
        /// </summary>
        public string ObjectTypeName 
        { 
            get { return objectTypeName; } 
            set { objectTypeName = value; }
        }
        protected string objectTypeName;

        /// <summary>
        /// Turns change-tracking on or off
        /// </summary>
        public bool TrackChanges
        {
            get { return trackChanges; }
            set { trackChanges = value; }
        }
        protected bool trackChanges;

        /// <summary>
        /// Set by derived class - indicates that the state of the object has changed
        /// </summary>
        public bool IsChanged { get { return isChanged; } }
        protected bool isChanged;

        /// <summary>
        /// Indicates whether or not an error exists
        /// </summary>
        public bool HasError { get { return errorFields.Any(); } }

        /// <summary>
        /// List of fields in an error state
        /// </summary>
        public List<string> ErrorFields { get { return errorFields; } }
        protected List<string> errorFields;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ObservableObject()
        {
            id = Guid.NewGuid();
            errorFields = new List<string>();
        }

        /// <summary>
        /// Constructor with initialization
        /// </summary>
        /// <param name="existingId">Typically used if Id is stored as persistent data</param>
        public ObservableObject(Guid existingId)
        {
            id = existingId;
            errorFields = new List<string>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compares oldVal and newVal and sets isChanged to true if they are not equal
        /// </summary>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        public void SetChangedState(object oldVal, object newVal)
        {
            isChanged = Equals(oldVal, newVal) == false;
            RaisePropertyChangedEvent("IsChanged");
        }

        /// <summary>
        /// Sets HasError state on or off
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorState"></param>
        public void SetErrorState(string propertyName, bool errorState)
        {
            bool fieldHasError = errorFields.Any(x => x == propertyName);
            if (fieldHasError && errorState == false)
            {
                errorFields.Remove(propertyName);
            }
            else if (fieldHasError == false && errorState == true)
            {
                errorFields.Add(propertyName);
            }

            if (errorState == true)
            {
                RaiseErrorExistsEvent(propertyName);
            }
            RaisePropertyChangedEvent("HasError");
        }
        #endregion
    }
}
