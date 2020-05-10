CREATE TABLE "AspNetUsers" ("Id" text PRIMARY KEY, "AccessFailedCount" int, "ConcurrencyStamp" text, "Email" text, "EmailConfirmed" boolean, "FirstName" text, "LastName" text,"LockoutEnabled" boolean,
                    "LockoutEnd" timestamp,
                    "NormalizedEmail" text,
                    "NormalizedUserName" text,
                    "PasswordHash" text,
                    "PhoneNumber" text,
                    "PhoneNumberConfirmed" boolean,
                    "SecurityStamp" text,
                    "TwoFactorEnabled" boolean,
                    "UserName" text);

CREATE TABLE "AspNetRoles" ("Id" TEXT PRIMARY KEY,
                    "ConcurrencyStamp" TEXT,
                    "Name" TEXT,
                    "NormalizedName" TEXT);

CREATE TABLE "AspNetUserTokens" ("UserId" TEXT,
                    "LoginProvider" TEXT,
                    "Name" TEXT,
                    "Value" TEXT);

CREATE TABLE "UserRole" ("Id" SERIAL PRIMARY KEY,
                              "IdentityId" TEXT REFERENCES "AspNetUsers"("Id"),
                    "Location" TEXT);

CREATE TABLE "AspNetUserClaims" ("Id" SERIAL PRIMARY KEY,
                                 "ClaimType" TEXT,
                    "ClaimValue" TEXT,
                    "UserId" TEXT REFERENCES "AspNetUsers"("Id"));
            

CREATE TABLE "AspNetUserLogins" ("LoginProvider" TEXT PRIMARY KEY,
                    "ProviderKey" TEXT,
                    "ProviderDisplayName" TEXT,
                    "UserId" TEXT REFERENCES "AspNetUsers"("Id"));

CREATE TABLE "AspNetRoleClaims" ("Id" TEXT PRIMARY KEY,
                    "ClaimType" TEXT,
                    "ClaimValue" TEXT,
                    "RoleId" TEXT REFERENCES "AspNetRoles"("Id"));

CREATE TABLE "AspNetUserRoles" ("UserId" TEXT REFERENCES "AspNetUsers"("Id") ,
                    "RoleId" TEXT REFERENCES "AspNetRoles"("Id"));

