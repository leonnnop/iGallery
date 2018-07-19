﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Moment
    {
        public string ID { get; set; }
        public string SenderID { get; set; }
        public string Content { get; set; }
        public int LikeNum { get; set; }
        public int ForwardNum { get; set; }
        public int CollectNum { get; set; }
        public int CommentNum { get; set; }
        public DateTime Time { get; set; }

        public Moment() { }
        public Moment(string id, string sender_id, string content, int likes, int forwards, int collects, int comments, DateTime time)
        {
            ID = id;
            SenderID = sender_id;
            Content = content;
            LikeNum = likes;
            ForwardNum = forwards;
            CollectNum = collects;
            CommentNum = comments;
            Time = time;
        }
    }
    /*测试
    
	"ID":"",
	"SenderID":"",
	"Content":"",
	"LikeNum":0,
	"ForwardNum":0,
	"CollectNum":0,
	"CommentNum":0,
	"Time":""

    */
}