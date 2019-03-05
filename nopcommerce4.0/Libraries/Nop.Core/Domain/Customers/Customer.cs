using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Z_Consultant;
using Nop.Core.Domain.Z_Harag;

namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class Customer : BaseEntity
    {
        private ICollection<ExternalAuthenticationRecord> _externalAuthenticationRecords;
        private ICollection<CustomerRole> _customerRoles;
        private ICollection<ShoppingCartItem> _shoppingCartItems;
        private ICollection<ReturnRequest> _returnRequests;
        private ICollection<Address> _addresses;

        /// <summary>
        /// Ctor
        /// </summary>
        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
            CustomersComments = new Collection<Z_Consultant_Comment>();
            ConsultantsComments = new Collection<Z_Consultant_Comment>();
            ConsultantsPosts = new Collection<Z_Consultant_Post>();
            CustomersPosts = new Collection<Z_Consultant_Post>();
            this.Z_Harag_BlackList = new HashSet<Z_Harag_BlackList>();
            this.Z_Harag_Comment = new HashSet<Z_Harag_Comment>();
           
            this.Z_Harag_CommentReport = new HashSet<Z_Harag_CommentReport>();
            this.Z_Harag_Favorite = new HashSet<Z_Harag_Favorite>();
            this.Z_Harag_Message = new HashSet<Z_Harag_Message>();
            this.Z_Harag_Notification = new HashSet<Z_Harag_Notification>();
            this.Z_Harag_User_Rate = new HashSet<Z_Harag_Rate>();
            this.Z_Harag_Rate = new HashSet<Z_Harag_Rate>();
            this.Z_Harag_Reports = new HashSet<Z_Harag_Reports>();
            this.Z_Harag_Posts = new HashSet<Z_Harag_Post>();
            this.CustomersHaragPayment = new HashSet<Z_Harag_BankPayment>();
            this.HaragFollowingUsers = new HashSet<Z_Harag_Follow>();
            this.HaragFollowedUsers = new HashSet<Z_Harag_Follow>();
            this.HaragCustomerNotification = new HashSet<Z_Harag_Notification>();
            this.OwnerNotification = new Collection<Z_Consultant_Notification>();
            this.UserNotification = new Collection<Z_Consultant_Notification>();

        }

        /// <summary>
        /// Gets or sets the customer GUID
        /// </summary>
        public Guid CustomerGuid { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        public string Mobile { get; set; }
        /// <summary>
        /// Gets or sets the email that should be re-validated. Used in scenarios when a customer is already registered and wants to change an email address.
        /// </summary>
        public string EmailToRevalidate { get; set; }

     
        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is tax exempt
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier
        /// </summary>
        public int AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the vendor identifier with which this customer is associated (maganer)
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this customer has some products in the shopping cart
        /// <remarks>The same as if we run this.ShoppingCartItems.Count > 0
        /// We use this property for performance optimization:
        /// if this property is set to false, then we do not need to load "ShoppingCartItems" navigation property for each page load
        /// It's used only in a couple of places in the presenation layer
        /// </remarks>
        /// </summary>
        public bool HasShoppingCartItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is required to re-login
        /// </summary>
        public bool Blocked { get; set; }
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a customer cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer account is system
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Gets or sets the customer system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }

        /// <summary>
        ///  Gets or sets the store identifier in which customer registered
        /// </summary>
        public int RegisteredInStoreId { get; set; }

        #region Navigation properties

        /// <summary>
        /// Gets or sets customer generated content
        /// </summary>
        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords
        {
            get { return _externalAuthenticationRecords ?? (_externalAuthenticationRecords = new List<ExternalAuthenticationRecord>()); }
            protected set { _externalAuthenticationRecords = value; }
        }

        /// <summary>
        /// Gets or sets the customer roles
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }

        /// <summary>
        /// Gets or sets shopping cart items
        /// </summary>
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItems = value; }
        }

        /// <summary>
        /// Gets or sets return request of this customer
        /// </summary>
        public virtual ICollection<ReturnRequest> ReturnRequests
        {
            get { return _returnRequests ?? (_returnRequests = new List<ReturnRequest>()); }
            protected set { _returnRequests = value; }
        }

        /// <summary>
        /// Default billing address
        /// </summary>
        public virtual Address BillingAddress { get; set; }

        /// <summary>
        /// Default shipping address
        /// </summary>
        public virtual Address ShippingAddress { get; set; }


        public ICollection<Z_Consultant_Comment> CustomersComments { get; set; }
        public ICollection<Z_Consultant_Comment> ConsultantsComments { get; set; }
        public ICollection<Z_Consultant_Post> CustomersPosts { get; set; }
        public ICollection<Z_Harag_BankPayment> CustomersHaragPayment { get; set; }
        public ICollection<Z_Consultant_Post> ConsultantsPosts { get; set; }
        // public ICollection<Z_Harag_Reports> CustomerReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_BlackList> Z_Harag_BlackList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Comment> Z_Harag_Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Z_Harag_Comment> Z_Harag_Comment1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Z_Harag_Comment> Z_Harag_Comment2 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_CommentReport> Z_Harag_CommentReport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Favorite> Z_Harag_Favorite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Message> Z_Harag_Message { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Notification> Z_Harag_Notification { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Z_Harag_Notification> Z_Harag_Notification1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Rate> Z_Harag_Rate { get; set; }
        public virtual ICollection<Z_Harag_Rate> Z_Harag_User_Rate { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Reports> Z_Harag_Reports { get; set; }
        public  ICollection<Z_Harag_Post> Z_Harag_Posts { get; set; }
        public ICollection<Z_Consultant_Notification> OwnerNotification { get; set; }
        public ICollection<Z_Consultant_Notification> UserNotification { get; set; }


        /// <summary>
        /// Gets or sets customer addresses
        /// </summary>
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            protected set { _addresses = value; }
        }

        public ICollection<Z_Harag_Follow> HaragFollowingUsers { get; set; }
        public ICollection<Z_Harag_Follow> HaragFollowedUsers { get; set; }
        public ICollection<Z_Harag_Notification> HaragCustomerNotification { get; set; }

        #endregion
    }
}