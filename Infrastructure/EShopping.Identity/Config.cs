// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace EShopping.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalogapi"),
                new ApiScope("basketapi")
            };


        public static IEnumerable<ApiResource> AppApiResources =>
            new ApiResource[]
            {
                //List of Microservices can go here
                new ApiResource("Catalog", "Catalog.API")
                {
                    Scopes = { "catalogapi" }
                },
                new ApiResource("Basket", "Basket.API")
                {
                    Scopes = { "basketapi" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //m2m Flow
                new Client
                {
                    ClientName = "Catalog API Client",
                    ClientId = "CatalogApiClient",
                    ClientSecrets = { new Secret("12345".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalogapi","basketapi"}
                }


            };
    }
}