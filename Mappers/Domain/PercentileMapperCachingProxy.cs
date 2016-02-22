﻿using System.Collections.Generic;

namespace DungeonGen.Mappers.Domain
{
    public class PercentileMapperCachingProxy : PercentileMapper
    {
        private PercentileMapper innerMapper;
        private Dictionary<string, Dictionary<int, string>> cachedTables;

        public PercentileMapperCachingProxy(PercentileMapper innerMapper)
        {
            this.innerMapper = innerMapper;
            cachedTables = new Dictionary<string, Dictionary<int, string>>();
        }

        public Dictionary<int, string> Map(string tableName)
        {
            if (cachedTables.ContainsKey(tableName) == false)
                cachedTables[tableName] = innerMapper.Map(tableName);

            return cachedTables[tableName];
        }
    }
}
