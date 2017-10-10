using Shields.GraphViz.Components;
using Shields.GraphViz.Models;
using Shields.GraphViz.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GramaticasFormales
{
    class GraphSet
    {
        public List<int> nodes;
        public List<Arista> aristas;

        public GraphSet()
        {
            this.nodes = new List<int>();
            this.aristas = new List<Arista>();
        }

        public void RemoveLastNode()
        {
            aristas.RemoveAll(e => e.NodEnd == nodes.Count);
            nodes.RemoveAt(nodes.Count - 1);
        }


        public void RemoveFirstNode()
        {
            aristas.RemoveAll(e => e.NodStart == 1);
            nodes.RemoveAt(0);
        }
    }

    class Arista
    {
        public int NodStart;
        public int NodEnd;
        public string AristaValue;

        public Arista(int nodStart, int nodEnd, string aristaValue)
        {
            NodStart = nodStart;
            NodEnd = nodEnd;
            AristaValue = aristaValue;
        }
    }
}
