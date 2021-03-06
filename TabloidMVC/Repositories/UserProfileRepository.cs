﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                                               u.CreateDateTime, u.ImageLocation, u.UserTypeId, u.IsDeactivated,
                                               ut.[Name] AS UserTypeName
                                          FROM UserProfile u
                                     LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                                         WHERE IsDeactivated = 0
                                      ORDER BY u.DisplayName";

                    var reader = cmd.ExecuteReader();

                    var users = new List<UserProfile>();

                    while (reader.Read())
                    {
                        users.Add(new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        });
                    }

                    reader.Close();
                    return users;
                }
            }
        }

        public List<UserProfile> GetDeactivated()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                                               u.CreateDateTime, u.ImageLocation, u.UserTypeId, u.IsDeactivated,
                                               ut.[Name] AS UserTypeName
                                          FROM UserProfile u
                                     LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                                         WHERE IsDeactivated = 1
                                      ORDER BY u.DisplayName";
                    var reader = cmd.ExecuteReader();

                    var users = new List<UserProfile>();

                    while (reader.Read())
                    {
                        users.Add(new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        });
                    }

                    reader.Close();
                    return users;
                }
            }
        }

        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId, u.IsDeactivated,
                              ut.[Name] AS UserTypeName
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE email = @email";
                    cmd.Parameters.AddWithValue("@email", email);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            IsDeactivated = reader.GetInt32(reader.GetOrdinal("IsDeactivated")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE u.id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }
        
        public void AddUser(UserProfile user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserProfile (FirstName, LastName, DisplayName, Email, CreateDateTime, ImageLocation, UserTypeId, IsDeactivated)
                    OUTPUT INSERTED.ID
                    VALUES (@FirstName, @LastName, @DisplayName, @Email, @CreateDateTime, @ImageLocation, @UserTypeId, 0);
                ";

                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@DisplayName", user.DisplayName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@CreateDateTime", user.CreateDateTime);
                    cmd.Parameters.AddWithValue("@UserTypeId", user.UserTypeId);
                    cmd.Parameters.AddWithValue("@ImageLocation", DbUtils.ValueOrDBNull(user.ImageLocation));

                    int id = (int)cmd.ExecuteScalar();

                    user.Id = id;
                }
            }
        }

        /*
        public void UpdateUser(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile
                                           SET DisplayName = @displayName,
	                                           FirstName = @firstName,
	                                           LastName = @lastName,
	                                           Email = @email,
	                                           CreateDateTime = @createDateTime,
	                                           ImageLocation = @imageLocation,
	                                           UserTypeId = @userTypeId,
	                                           IsDeactivated = @isDeactivated
	   
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@displayName", user.DisplayName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@createDateTime", user.CreateDateTime);
                    cmd.Parameters.AddWithValue("@userTypeId", user.UserTypeId);
                    cmd.Parameters.AddWithValue("@imageLocation", DbUtils.ValueOrDBNull(user.ImageLocation));
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        */

        
        public void UpdateUser(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DECLARE @Admins INT
                        SELECT @Admins = Count(*)
                        FROM UserProfile
                        WHERE UserTypeId = 1 AND IsDeactivated = 0
                        IF @Admins = 1 
                            IF (SELECT UserTypeId FROM UserProfile WHERE id = @id) = 1 AND @userTypeId = 2
                                THROW 51000, 'There must be at least 1 admin', 1
                            ELSE
                                UPDATE UserProfile
	                            SET UserTypeId = @userTypeId	   
                                WHERE Id = @id                              
                        ELSE
                            UPDATE UserProfile
	                        SET UserTypeId = @userTypeId	   
                            WHERE Id = @id ";
                    
                    /* IF there is only one admin in database 
                           IF editing an admin and changing their usertype to Author
                               THROW an error
                           ELSE
                               update user
                       ELSE
                           update user */

                    cmd.Parameters.AddWithValue("@userTypeId", user.UserTypeId);
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        //Soft delete, sends user to "deactivated" page
        public void DeleteUser(int id)
        {
                using (var conn = Connection)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DECLARE @Admins INT
                            SELECT @Admins = Count(*)
                            FROM UserProfile
                            WHERE UserTypeId = 1 AND IsDeactivated = 0
                            IF @Admins = 1 AND (SELECT UserTypeId FROM UserProfile WHERE id = @id) = 1
                                THROW 51000, 'There must be at least 1 admin', 1
                            ELSE
                                UPDATE UserProfile
                                SET IsDeactivated = @IsDeactivated
                                WHERE id = @id";

                        cmd.Parameters.AddWithValue("@IsDeactivated", 1);
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
        }

        public void ReactivateUser(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile
                                           SET IsDeactivated = @IsDeactivated
                                        WHERE id = @id";

                    cmd.Parameters.AddWithValue("@IsDeactivated", 0);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
