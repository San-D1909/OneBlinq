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
                    string[] lineData = line.Split(":");
                    if (lineData.Length > 2)
                    {
                        throw new ArgumentException($"There may be no : used in a parameter of filterKey or filterValue but this key is reserved for the JSON.");
                    }

                    string key = lineData[0].Replace("\"", "");
                    string v = lineData[1].Replace("\"", "\"");
                
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

        public bool Filter<TModel>(TModel entity, Dictionary<string, object> filters)
        {
            Type modelType = typeof(TModel);
            IList<PropertyInfo> props = new List<PropertyInfo>(modelType.GetProperties());

            List<bool> propMatch = new List<bool>();
            foreach(KeyValuePair<string, object> pair in filters)
            {
                if (props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).Count() != 0)
                {
                    if (pair.Value.GetType() == typeof(string))
                    {
                        string v = (string)pair.Value;
                        PropertyInfo p = props.Where(p => p.Name.ToLower() == pair.Key.ToLower()).First();
                        return ((string)p.GetValue(entity)) == v;
                    }else
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
                    foreach (string v in ((string[]) pair.Value))
                    {
                        compareResults.Add(v == p.GetValue(entity).ToString());
                    }

                    return compareResults.Contains(true);
                }
                else
                {
                   throw new ArgumentException($"Property: {pair.Key} doesn't exist in Entity {modelType.Name}");
                }
            }


            return false;
        }

        public TModel SortDatabaseModel<TModel>(ref TModel entity, string sort)
        {
            return entity;
        }
    }
}
