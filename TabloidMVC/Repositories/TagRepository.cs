using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration config) : base(config) { }
        public List<Tag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Tag ORDER BY Name ASC";
                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        public List<Tag> GetPostTags(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Tag.Id, Tag.Name From Tag
                        LEFT JOIN PostTag on Tag.Id= PostTag.TagId
                        WHERE PostTag.PostId = @postId;";
                    cmd.Parameters.AddWithValue("@postId", postId);

                    var reader = cmd.ExecuteReader();
                    List<Tag> tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        });
                    }
                    reader.Close();
                    return tags;
                }
            }
        }

        public void AddPostTag(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO PostTag (PostId,TagId) 
                    OUTPUT INSERTED.ID
                    VALUES (@postId,@tagId);
                    ";

                    cmd.Parameters.AddWithValue("@postId", postTag.PostId);
                    cmd.Parameters.AddWithValue("@tagId", postTag.TagId);

                    int newlyCreatedPostTagId = (int)cmd.ExecuteScalar();

                    postTag.Id = newlyCreatedPostTagId;
                }
            }
        }

        public void DeletePostTag(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     DELETE FROM PostTag Where PostId = @postId AND TagId=@tagId;
                        ";
                    cmd.Parameters.AddWithValue("@postId", postTag.PostId);
                    cmd.Parameters.AddWithValue("@tagId", postTag.TagId);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTag(int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     DELETE FROM Tag Where Id = @tagId;
                        ";
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Tag GetTagById(int tagId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Tag Where Id=@tagId;";
                    
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Tag tag = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        };
                        reader.Close();
                        return tag;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public void UpdateTag(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Tag
                            SET 
                                [Name] = @name
                            WHERE Id = @tagId";

                    cmd.Parameters.AddWithValue("@name", tag.Name);
                    cmd.Parameters.AddWithValue("@tagId", tag.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddTag(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Tag ([Name]) 
                    OUTPUT INSERTED.ID
                    VALUES (@name);
                    ";

                    cmd.Parameters.AddWithValue("@name", tag.Name);

                    int newlyCreatedTagId = (int)cmd.ExecuteScalar();

                    tag.Id = newlyCreatedTagId;
                }
            }
        }
    }
}
