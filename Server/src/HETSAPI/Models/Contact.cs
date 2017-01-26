/*
 * REST API Documentation for the MOTI Hired Equipment Tracking System (HETS) Application
 *
 * The Hired Equipment Program is for owners/operators who have a dump truck, bulldozer, backhoe or  other piece of equipment they want to hire out to the transportation ministry for day labour and  emergency projects.  The Hired Equipment Program distributes available work to local equipment owners. The program is  based on seniority and is designed to deliver work to registered users fairly and efficiently  through the development of local area call-out lists. 
 *
 * OpenAPI spec version: v1
 * 
 * 
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace HETSAPI.Models
{
    /// <summary>
    /// 
    /// </summary>

    public partial class Contact : IEquatable<Contact>
    {
        /// <summary>
        /// Default constructor, required by entity framework
        /// </summary>
        public Contact()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact" /> class.
        /// </summary>
        /// <param name="Id">Primary Key (required).</param>
        /// <param name="GivenName">The given name of the contact..</param>
        /// <param name="Surname">The surname of the contact..</param>
        /// <param name="Role">The role of the contact. UI controlled as to whether it is free form or selected from an enumerated list..</param>
        /// <param name="Notes">Notes about the contact..</param>
        /// <param name="Phones">Phones.</param>
        /// <param name="Addresses">Addresses.</param>
        public Contact(int Id, string GivenName = null, string Surname = null, string Role = null, string Notes = null, List<ContactPhone> Phones = null, List<ContactAddress> Addresses = null)
        {   
            this.Id = Id;
            this.GivenName = GivenName;
            this.Surname = Surname;
            this.Role = Role;
            this.Notes = Notes;
            this.Phones = Phones;
            this.Addresses = Addresses;
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        /// <value>Primary Key</value>
        [MetaDataExtension (Description = "Primary Key")]
        public int Id { get; set; }
        
        /// <summary>
        /// The given name of the contact.
        /// </summary>
        /// <value>The given name of the contact.</value>
        [MetaDataExtension (Description = "The given name of the contact.")]
        public string GivenName { get; set; }
        
        /// <summary>
        /// The surname of the contact.
        /// </summary>
        /// <value>The surname of the contact.</value>
        [MetaDataExtension (Description = "The surname of the contact.")]
        public string Surname { get; set; }
        
        /// <summary>
        /// The role of the contact. UI controlled as to whether it is free form or selected from an enumerated list.
        /// </summary>
        /// <value>The role of the contact. UI controlled as to whether it is free form or selected from an enumerated list.</value>
        [MetaDataExtension (Description = "The role of the contact. UI controlled as to whether it is free form or selected from an enumerated list.")]
        public string Role { get; set; }
        
        /// <summary>
        /// Notes about the contact.
        /// </summary>
        /// <value>Notes about the contact.</value>
        [MetaDataExtension (Description = "Notes about the contact.")]
        public string Notes { get; set; }
        
        /// <summary>
        /// Gets or Sets Phones
        /// </summary>
        public List<ContactPhone> Phones { get; set; }
        
        /// <summary>
        /// Gets or Sets Addresses
        /// </summary>
        public List<ContactAddress> Addresses { get; set; }
        
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Contact {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  GivenName: ").Append(GivenName).Append("\n");
            sb.Append("  Surname: ").Append(Surname).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
            sb.Append("  Notes: ").Append(Notes).Append("\n");
            sb.Append("  Phones: ").Append(Phones).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }
            return Equals((Contact)obj);
        }

        /// <summary>
        /// Returns true if Contact instances are equal
        /// </summary>
        /// <param name="other">Instance of Contact to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Contact other)
        {

            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return                 
                (
                    this.Id == other.Id ||
                    this.Id.Equals(other.Id)
                ) &&                 
                (
                    this.GivenName == other.GivenName ||
                    this.GivenName != null &&
                    this.GivenName.Equals(other.GivenName)
                ) &&                 
                (
                    this.Surname == other.Surname ||
                    this.Surname != null &&
                    this.Surname.Equals(other.Surname)
                ) &&                 
                (
                    this.Role == other.Role ||
                    this.Role != null &&
                    this.Role.Equals(other.Role)
                ) &&                 
                (
                    this.Notes == other.Notes ||
                    this.Notes != null &&
                    this.Notes.Equals(other.Notes)
                ) && 
                (
                    this.Phones == other.Phones ||
                    this.Phones != null &&
                    this.Phones.SequenceEqual(other.Phones)
                ) && 
                (
                    this.Addresses == other.Addresses ||
                    this.Addresses != null &&
                    this.Addresses.SequenceEqual(other.Addresses)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks
                                   
                hash = hash * 59 + this.Id.GetHashCode();                if (this.GivenName != null)
                {
                    hash = hash * 59 + this.GivenName.GetHashCode();
                }                
                                if (this.Surname != null)
                {
                    hash = hash * 59 + this.Surname.GetHashCode();
                }                
                                if (this.Role != null)
                {
                    hash = hash * 59 + this.Role.GetHashCode();
                }                
                                if (this.Notes != null)
                {
                    hash = hash * 59 + this.Notes.GetHashCode();
                }                
                                   
                if (this.Phones != null)
                {
                    hash = hash * 59 + this.Phones.GetHashCode();
                }                   
                if (this.Addresses != null)
                {
                    hash = hash * 59 + this.Addresses.GetHashCode();
                }
                return hash;
            }
        }

        #region Operators
        
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Contact left, Contact right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Not Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Contact left, Contact right)
        {
            return !Equals(left, right);
        }

        #endregion Operators
    }
}
