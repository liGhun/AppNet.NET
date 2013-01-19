using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Entities
    {
        public interface IEntity
        {
            int pos { get; set; }
            int len { get; set; }
        }

        public List<Mention> mentions { get; set; }
        public List<Hashtag> hashtags { get; set; }
        public List<Link> links { get; set; }
        public List<IEntity> allEntities
        {
            get
            {
                if (_allEntities != null)
                {
                    return _allEntities;
                }
                else
                {
                    _allEntities = new List<IEntity>();
                    if (mentions != null)
                    {
                        foreach (Mention mention in mentions)
                        {
                            _allEntities.Add(mention);
                        }
                    }
                    if(hashtags != null) {
                        foreach (Hashtag hashtag in hashtags)
                        {
                            _allEntities.Add(hashtag);
                        }
                    }
                    if(links != null) {
                        foreach (Link link in links)
                        {
                            _allEntities.Add(link);
                        }
                    }
                    _allEntities.Sort();        
                
                    return _allEntities;
                }
            }
        }
        private List<IEntity> _allEntities { get; set; }

        public class Mention : IEntity, IComparable
        {
            public string name { get; set; }
            public string id { get; set; }
            public int pos { get; set; }
            public int len { get; set; }

            public int CompareTo(IEntity b)
            {
                // sorting by start point
                return this.pos.CompareTo(b.pos);
            }

            public int CompareTo(object obj)
            {
                IEntity entity = obj as IEntity;
                if (entity != null)
                {
                    return CompareTo(entity);
                }
                return this.pos;
            }
        }

        public class Hashtag : IEntity, IComparable
        {
            public string name { get; set; }
            public int pos { get; set; }
            public int len { get; set; }

            public int CompareTo(IEntity b)
            {
                // sorting by start point
                return this.pos.CompareTo(b.pos);
            }

            public int CompareTo(object obj)
            {
                IEntity entity = obj as IEntity;
                if (entity != null)
                {
                    return CompareTo(entity);
                }
                return this.pos;
            }

        }

        public class Link : IEntity, IComparable
        {
            public string text { get; set; }
            public string url { get; set; }
            public int pos { get; set; }
            public int len { get; set; }

            public int CompareTo(IEntity b)
            {
                // sorting by start point
                return this.pos.CompareTo(b.pos);
            }

            public int CompareTo(object obj)
            {
                IEntity entity = obj as IEntity;
                if (entity != null)
                {
                    return CompareTo(entity);
                }
                return this.pos;
            }
        }
    }
}
