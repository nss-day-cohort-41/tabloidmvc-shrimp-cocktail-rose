using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        
        public CommentRepository(IConfiguration config) : base(config) { }

        public List<Comment> GetAll(int id, IUserProfileRepository _userProfileRepository)
        {
            using (var conn = Connection)
            {

                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, PostId, UserProfileId, Subject, Content, CreateDateTime FROM Comment WHERE PostId = @id ORDER BY CreateDateTime Desc";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    List<Comment> AllComment = new List<Comment>();

                    while (reader.Read())
                    {
                        AllComment.Add(new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            User = _userProfileRepository.GetById(reader.GetInt32(reader.GetOrdinal("UserProfileId")))

                        });
                    }

                    reader.Close();

                    return AllComment;
                }
            }
        }

        public Comment GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                              c.id AS CommentId, c.PostId, c.Subject, c.Content,
                                              c.CreateDateTime AS CommentCDT, p.id AS UserProfileId, 
                                              p.DisplayName, p.FirstName, p.LastName, p.Email, 
                                              p.CreateDateTime, p.ImageLocation, p.UserTypeId, p.IsDeactivated
                                       FROM Comment c 
                                       JOIN UserProfile p ON c.UserProfileId = p.id 
                                       WHERE c.id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Comment comment = new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CommentId")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CommentCDT")),
                            User = new UserProfile
                            {
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        };
                        reader.Close();
                        return comment;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
        public void AddComment(Comment comment)
        {

            using (var conn = Connection)
            {

                conn.Open();

                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Comment (
 PostId, UserProfileId, Subject, Content, CreateDateTime)
VALUES (@PostId, @UserProfileId, @Subject, @Content, @CreateDateTime)";

                    cmd.Parameters.AddWithValue("@PostId",comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);

                    comment.Id = (int)cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Comment
                                           SET
                                               Subject = @subject,
                                               Content = @content
                                         WHERE id = @id";

                    cmd.Parameters.AddWithValue("@subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@id", comment.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }





    }
}
