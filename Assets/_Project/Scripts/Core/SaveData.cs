using System;
using System.Collections.Generic;
using System.Linq;

namespace DormGame.Core
{
    [Serializable]
    public class SaveData
    {
        // JsonUtility 兼容：使用数组而非 Dictionary
        public StringIntPair[] storyFlags = Array.Empty<StringIntPair>();
        public StringIntPair[] affinityFlags = Array.Empty<StringIntPair>();
        public List<string> playedScripts = new List<string>();

        // 运行时辅助方法
        public Dictionary<string, int> GetStoryFlagsDict()
        {
            return storyFlags.ToDictionary(p => p.key, p => p.value);
        }

        public void SetStoryFlagsDict(Dictionary<string, int> dict)
        {
            storyFlags = dict.Select(kv => new StringIntPair(kv.Key, kv.Value)).ToArray();
        }

        public Dictionary<string, int> GetAffinityFlagsDict()
        {
            return affinityFlags.ToDictionary(p => p.key, p => p.value);
        }

        public void SetAffinityFlagsDict(Dictionary<string, int> dict)
        {
            affinityFlags = dict.Select(kv => new StringIntPair(kv.Key, kv.Value)).ToArray();
        }
    }

    [Serializable]
    public struct StringIntPair
    {
        public string key;
        public int value;

        public StringIntPair(string k, int v)
        {
            key = k;
            value = v;
        }
    }
}
