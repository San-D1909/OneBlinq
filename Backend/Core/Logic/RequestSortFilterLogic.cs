using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.Core.Logic
{
    public class RequestSortFilterLogic
    {
        public IEnumerable<TModel> FilterDatabaseModel<TModel>(IEnumerable<TModel> entity, string filter)
        {
            if (filter != null && filter != "{}")
            {
                string converted_filter = filter;
                string pattern = @"(?<=\[)(.*?)(?=\])";

                Dictionary<string, object> f = new Dictionary<string, object>();

                MatchCollection matches = Regex.Matches(converted_filter, pattern);
                foreach(Match match in matches)
                {
                    if (match.Success)
                    {
                        string replacement = match.Value.Replace(",", ";");
                        converted_filter = Regex.Replace(converted_filter, pattern, replacement);
                    }
                }
                string[] lines = converted_filter.Split(",");
                foreach(string line in lines)
                {
                    string filterline = line.Replace(":", ".");
                    string v = filterline.Substring(filterline.LastIndexOf(".") + 1).Replace("\"", "");
                    string key = filterline.Substring(0, filterline.LastIndexOf(".")).Replace("\"", "");

                    //string key = lineData[0].Replace("\"", "");
                    //string v = lineData[1].Replace("\"", "\"");
                
                    if (v.Contains("["))
                    {
                        f.Add(key.Replace("{", "").Replace("}", ""), v.Replace("\"", "").Replace("[", "").Replace("]", "").Replace(" ", "").Replace("{", "").Replace("}", "").Split(";"));
                    }else
                    {
                        f.Add(key.Replace("{", "").Replace("}", ""), v.Replace("\"", "").Replace("[", "").Replace("]", "").Replace(" ", "").Replace("{", "").Replace("}", "").Split(";"));
                    }

                
                }

                return entity.Where(x => Filter(x, f)).ToList();
            }

            return entity;
        }

        private bool Filter(object entity, Type entityType, Dictionary<string, object> filters)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(entityType.GetProperties());
            IList<PropertyInfo> subentities = new List<PropertyInfo>(entityType.GetProperties()).Where(prop => prop.PropertyType.IsClass).ToList();
            List<bool> propMatch = new List<bool>();
            foreach (KeyValuePair<string, object> pair in filters)
            {
                if (props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).Count() != 0)
                {
                    if (pair.Value.GetType() == typeof(string))
                    {
                        string v = (string)pair.Value;
                        PropertyInfo p = props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).First();
                        return ((string)p.GetValue(entity)) == v;
                    }
                    else
                    {
                        List<bool> compareResults = new List<bool>();
                        PropertyInfo p = props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).First();
                        foreach (string v in ((string[])pair.Value))
                        {
                            object? dbvalue = p.GetValue(entity);
                            if (dbvalue is string)
                            {
                                compareResults.Add(dbvalue.ToString().ToLower().Contains(v.ToLower()));
                            }
                            else
                            {
                                compareResults.Add(v.ToLower() == dbvalue.ToString().ToLower());
                            }

                        }

                        return compareResults.Contains(true);
                    }
                }
                else if (props.Where(p => p.Name.ToLower() + "s" == pair.Key.ToLower() || p.Name.ToLower() + "en" == pair.Key.ToLower()).Count() != 0)
                {
                    List<bool> compareResults = new List<bool>();
                    PropertyInfo p = props.Where(p => p.Name.ToLower() + "s" == pair.Key.ToLower() || p.Name.ToLower() + "en" == pair.Key.ToLower()).First();
                    foreach (string v in ((string[])pair.Value))
                    {
                        compareResults.Add(v == p.GetValue(entity).ToString());
                    }

                    return compareResults.Contains(true);
                }
                else
                {
                    string layers = pair.Key.Substring(pair.Key.IndexOf('.') + 1);
                    string source = pair.Key.Substring(0, pair.Key.IndexOf('.'));
                    if (subentities.Where(p => p.Name.ToLower() == source.ToLower()).Count() != 0)
                    {
                        PropertyInfo propinfo = subentities.Where(p => p.Name.ToLower() == source.ToLower()).First();
                        return CheckLayer(propinfo.GetValue(entity), source, layers, pair.Value, propinfo.PropertyType);
                    }
                    else
                    {
                        throw new ArgumentException($"Property: {pair.Key} doesn't exist in Entity {entity.GetType().Name}");
                    }
                }
            }


            return false;
        }

        public bool Filter<TModel>(TModel entity, Dictionary<string, object> filters)
        {
            Type modelType = typeof(TModel);
            return Filter(entity, modelType, filters);
            //IList<PropertyInfo> props = new List<PropertyInfo>(modelType.GetProperties());
            //IList<PropertyInfo> subentities = new List<PropertyInfo>(modelType.GetProperties()).Where(prop => prop.PropertyType.IsClass).ToList();
            //List<bool> propMatch = new List<bool>();
            //foreach(KeyValuePair<string, object> pair in filters)
            //{
            //    if (props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).Count() != 0)
            //    {
            //        if (pair.Value.GetType() == typeof(string))
            //        {
            //            string v = (string)pair.Value;
            //            PropertyInfo p = props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).First();
            //            return ((string)p.GetValue(entity)) == v;
            //        }else
            //        {
            //            List<bool> compareResults = new List<bool>();
            //            PropertyInfo p = props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).First();
            //            foreach (string v in ((string[])pair.Value))
            //            {
            //                object? dbvalue = p.GetValue(entity);
            //                if (dbvalue is string)
            //                {
            //                    compareResults.Add(dbvalue.ToString().ToLower().Contains(v.ToLower()));
            //                }
            //                else
            //                {
            //                    compareResults.Add(v.ToLower() == dbvalue.ToString().ToLower());
            //                }
                            
            //            }

            //            return compareResults.Contains(true);
            //        }
            //    }
            //    else if (props.Where(p => p.Name.ToLower() + "s" == pair.Key.ToLower() || p.Name.ToLower() + "en" == pair.Key.ToLower()).Count() != 0)
            //    {
            //        List<bool> compareResults = new List<bool>();
            //        PropertyInfo p = props.Where(p => p.Name.ToLower() + "s" == pair.Key.ToLower() || p.Name.ToLower() + "en" == pair.Key.ToLower()).First();
            //        foreach (string v in ((string[]) pair.Value))
            //        {
            //            compareResults.Add(v == p.GetValue(entity).ToString());
            //        }

            //        return compareResults.Contains(true);
            //    }
            //    else
            //    {
            //        string layers = pair.Key.Substring(pair.Key.IndexOf('.') + 1);
            //        string source = pair.Key.Substring(0, pair.Key.IndexOf('.'));
            //        if (subentities.Where(p => p.Name.ToLower() == source.ToLower()).Count() != 0)
            //        {
            //            PropertyInfo propinfo = subentities.Where(p => p.Name.ToLower() == source.ToLower()).First();
            //            return CheckLayer(source, layers.Split("."), pair.Value, propinfo.PropertyType);
            //        } 
            //        else
            //        {
            //            throw new ArgumentException($"Property: {pair.Key} doesn't exist in Entity {modelType.Name}");
            //        }
            //    }
            //}


            //return false;
        }

        private bool CheckLayer(object entity, string propertyName, string keyLayer, object value, Type entityType)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add(keyLayer, value);
            return Filter(entity, entityType, filter);
            //return false;
        }
        public TModel SortDatabaseModel<TModel>(ref TModel entity, string sort)
        {
            return entity;
        }
    }
}
