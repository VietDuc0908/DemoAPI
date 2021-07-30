using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common
{
    public static class KendoGridFilterParse
    {
        public static KendoGridFilterModel Parse(Dictionary<string, StringValues> queryDictionary, string sortDefault = "CreatedDate desc")
        {
            var filterModel = new KendoGridFilterModel
            {
                Page = queryDictionary.ContainsKey("page") ? queryDictionary["page"].ToString().ToInt(0).Value : 0,
                PageSize = queryDictionary.ContainsKey("pageSize") ? queryDictionary["pageSize"].ToString().ToInt(0).Value : 0,
                Skip = queryDictionary.ContainsKey("skip") ? queryDictionary["skip"].ToString().ToInt(0).Value : 0,
                Take = queryDictionary.ContainsKey("take") ? queryDictionary["take"].ToString().ToInt(0).Value : 0,
                FilterItems = new List<FilterItem>(),
                Sort = sortDefault
            };

            var fiterItem = new FilterItem
            {
                Filters = new List<Filters>()
            };

            var count = queryDictionary.Count;
            count = count - 4 - ((count - 4) % 4);
            var loopCount = count / 4;
            for (int i = 0; i < loopCount; i++)
            {
                var filters = queryDictionary.Where(s => s.Key.Contains($"filter[filters][{i}]"));
                if (filters.Any())
                {
                    var loop = count <= 14 ? 1 : 2;

                    for (int j = 0; j < loop; j++)
                    {
                        var fieldKey = GetFieldKey(count, i, j, "field");
                        var operatorKey = GetFieldKey(count, i, j, "operator");
                        var valueKey = GetFieldKey(count, i, j, "value");
                        var logicKey = $"filter[filters][{i}][logic]:and";

                        var fieldValue = filters.FirstOrDefault(s => s.Key.Equals(fieldKey));
                        if (!string.IsNullOrEmpty(fieldValue.Key))
                        {
                            var filter = new Filters
                            {
                                Field = filters.FirstOrDefault(s => s.Key.Equals(fieldKey)).Value,
                                Operator = filters.FirstOrDefault(s => s.Key.Equals(operatorKey)).Value,
                                Value = filters.FirstOrDefault(s => s.Key.Equals(valueKey)).Value,
                                Logic = filters.FirstOrDefault(s => s.Key.Equals(logicKey)).Value
                            };

                            if (string.IsNullOrEmpty(filter.Logic) && i % 2 == 0)
                            {
                                var logic = queryDictionary.FirstOrDefault(s => s.Key.Equals("filter[logic]"));
                                filter.Logic = logic.Value;
                            }
                            fiterItem.Filters.Add(filter);
                        }
                    }
                }
            }
            filterModel.FilterItems.Add(fiterItem);
            if (queryDictionary.Keys.Any(s => s.Contains("sort")))
            {
                var sorts = queryDictionary.Where(s => s.Key.Contains($"sort[0]"));
                var field = sorts.FirstOrDefault(s => s.Key.Equals("sort[0][field]")).Value;
                var dir = sorts.FirstOrDefault(s => s.Key.Equals("sort[0][dir]")).Value;
                filterModel.Sort = $"{field} {dir}";
            }
            return filterModel;
        }

        private static string GetFieldKey(int count, int iIndex, int jIndex, string field)
        {
            if (count <= 14)
            {
                return $"filter[filters][{iIndex}][{field}]";
            }
            else
            {
                return $"filter[filters][{iIndex}][filters][{jIndex}][{field}]";
            }
        }
    }
}
