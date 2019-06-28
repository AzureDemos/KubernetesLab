using k8s.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKSWebsite.Models
{
    public class ClusterResponse
    {
        public List<PodCollection> PodCollections { get; set; } = new List<PodCollection>();
        public string Error { get; set; }
    }

    public class PodCollection
    {
        public string Name { get; set; }
        public List<V1Pod> Pods { get; set; } = new List<V1Pod>();
    }
}
