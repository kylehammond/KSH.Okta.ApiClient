using System;
using Newtonsoft.Json;

namespace KSH.Okta.ApiClient.TokenAuth.Models
{
    /// <summary>
    ///     Generated based on Okta docs
    ///     <see>https://developer.okta.com/docs/api/resources/users#user-model</see>
    /// </summary>
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("activated")]
        public DateTimeOffset? Activated { get; set; }

        [JsonProperty("statusChanged")]
        public DateTimeOffset? StatusChanged { get; set; }

        [JsonProperty("lastLogin")]
        public DateTimeOffset? LastLogin { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("passwordChanged")]
        public DateTimeOffset? PasswordChanged { get; set; }

        [JsonProperty("transitioningToStatus")]
        public string TransitioningToStatus { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

    }

    public class Profile
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("secondEmail")]
        public string SecondEmail { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("honorificPrefix")]
        public string HonorificPrefix { get; set; }

        [JsonProperty("honorificSuffix")]
        public string HonorificSuffix { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("profileUrl")]
        public string ProfileUrl { get; set; }

        [JsonProperty("primaryPhone")]
        public string PrimaryPhone { get; set; }

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("employeeNumber")]
        public string EmployeeNumber { get; set; }

        [JsonProperty("costCenter")]
        public string CostCenter { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("division")]
        public string Division { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("managerId")]
        public string ManagerId { get; set; }

        [JsonProperty("manager")]
        public string Manager { get; set; }
    }
}