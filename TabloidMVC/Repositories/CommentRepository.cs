﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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


    }
}