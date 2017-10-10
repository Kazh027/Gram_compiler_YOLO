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
    
    class GraphGen
    {
        IRenderer renderer;
        public List<char> postfija;
        public GraphSet myGraph;
        public Image CurrentImage;

        public GraphGen(List<char> CharsL)
        {
            renderer = new Renderer(@"C:\Program Files (x86)\Graphviz2.38\bin");
            postfija = CharsL;
            CurrentImage = null;
            myGraph = new GraphSet();
        }

        public GraphSet TommyGraphGen(List<char> Postfija)
        {
            Stack<GraphSet> MainStack = new Stack<GraphSet>();
            Stack<GraphSet> AuxStack = new Stack<GraphSet>();
            GraphSet gpTemp1 = new GraphSet();
            GraphSet gpTemp2 = new GraphSet();
            for (int it = 0; it < Postfija.Count(); it++)
            {
                switch (Postfija[it])
                {
                    case ('.'):
                        gpTemp2 = new GraphSet();
                        gpTemp1 = new GraphSet();
                        gpTemp2 = MainStack.Pop();
                        gpTemp1 = MainStack.Pop();
                        int firstCount = gpTemp1.nodes.Count()-1;

                        foreach (Arista itAr in gpTemp2.aristas)
                        {
                            itAr.NodStart += firstCount;
                            itAr.NodEnd += firstCount;
                            /**************************/
                            gpTemp1.aristas.Add(itAr);
                        }
                        for (int i = 0; i < gpTemp2.nodes.Count(); i++)
                        {
                            gpTemp2.nodes[i] += firstCount;
                            /**************************/
                            if (!gpTemp1.nodes.Contains(gpTemp2.nodes[i]))
                                gpTemp1.nodes.Add(gpTemp2.nodes[i]);
                        }
                        MainStack.Push(gpTemp1);
                        break;
                    case ('|'):
                        /********OR***************/
                        gpTemp2 = new GraphSet();
                        gpTemp1 = new GraphSet();
                        gpTemp2 = MainStack.Pop();
                        gpTemp1 = MainStack.Pop();
                         foreach (Arista itAr in gpTemp1.aristas)
                        {
                            itAr.NodStart++;
                            itAr.NodEnd++;
                        }
                        int FirstGpT = gpTemp1.nodes.Count();
                        foreach (Arista itAr in gpTemp2.aristas)
                        {
                            itAr.NodStart += FirstGpT+1;
                            itAr.NodEnd += FirstGpT+1;
                            /**************************/
                            gpTemp1.aristas.Add(itAr);
                        }
                        for (int i = 0; i < gpTemp2.nodes.Count(); i++)
                        {
                            gpTemp2.nodes[i] += FirstGpT;
                            /**************************/
                            if (!gpTemp1.nodes.Contains(gpTemp2.nodes[i]))
                                gpTemp1.nodes.Add(gpTemp2.nodes[i]);
                        }

                        gpTemp1.nodes.Insert(0,0);
                        gpTemp1.nodes.Add(gpTemp1.nodes.Count);

                        gpTemp1.aristas.Insert(0,new Arista(0, 1, "ε"));
                        gpTemp1.aristas.Insert(0,new Arista(0, FirstGpT + 1, "ε"));
                        gpTemp1.aristas.Add(new Arista(FirstGpT, gpTemp1.nodes.Count - 1, "ε"));
                        gpTemp1.aristas.Add(new Arista(gpTemp1.nodes.Count - 2, gpTemp1.nodes.Count - 1, "ε"));

                        MainStack.Push(gpTemp1);
                        /********OR***************/
                        break;
                    case ('ᵡ'):
                        gpTemp1 = new GraphSet();
                        gpTemp1 = MainStack.Pop();
                        foreach (Arista itAr in gpTemp1.aristas)
                        {
                            itAr.NodStart++;
                            itAr.NodEnd++;
                        }
                        for (int i = 0; i < gpTemp1.nodes.Count(); i++)                       
                            gpTemp1.nodes[i]++;
                        gpTemp1.nodes.Add(0);
                        gpTemp1.nodes.Add(gpTemp1.nodes.Count);
                        gpTemp1.aristas.Insert(0,new Arista(0, gpTemp1.nodes.Count - 1, "ε"));
                        gpTemp1.aristas.Insert(0, new Arista(0, 1, "ε"));
                        gpTemp1.aristas.Add(new Arista(gpTemp1.nodes.Count - 2, 1, "ε"));
                        gpTemp1.aristas.Add(new Arista(gpTemp1.nodes.Count - 2, gpTemp1.nodes.Count - 1, "ε"));
                        MainStack.Push(gpTemp1);
                        break;
                    case ('⁺'):
                        gpTemp1 = new GraphSet();
                        gpTemp1 = MainStack.Pop();
                        foreach (Arista itAr in gpTemp1.aristas)
                        {
                            itAr.NodStart++;
                            itAr.NodEnd++;
                        }
                        for (int i = 0; i < gpTemp1.nodes.Count(); i++)
                            gpTemp1.nodes[i]++;
                        gpTemp1.nodes.Add(0);
                        gpTemp1.nodes.Add(gpTemp1.nodes.Count);
                        gpTemp1.aristas.Insert(0, new Arista(0, 1, "ε"));
                        gpTemp1.aristas.Add(new Arista(gpTemp1.nodes.Count - 2, 1, "ε"));
                        gpTemp1.aristas.Add(new Arista(gpTemp1.nodes.Count - 2, gpTemp1.nodes.Count - 1, "ε"));
                        MainStack.Push(gpTemp1);
                        break;
                    case ('('):
                        gpTemp1 = new GraphSet();
                        int saveindx = it;
                        List<char> nextToPar = new List<char>();
                        for (int j = it + 1; j < Postfija.Count; j++)
                        {
                            if (Postfija[j] == ')')
                            {
                                saveindx = j;
                                j = Postfija.Count;
                            }
                            else
                                nextToPar.Add(Postfija[j]);
                        }                                       
                        it = saveindx;
                        gpTemp1 = TommyGraphGen(nextToPar);
                        MainStack.Push(gpTemp1);
                        break;
                    default:
                        gpTemp1 = new GraphSet();
                        gpTemp1.nodes.Add(0);
                        gpTemp1.nodes.Add(1);
                        gpTemp1.aristas.Add(new Arista(0, 1, Postfija[it].ToString()));
                        MainStack.Push(gpTemp1);
                        break;
                }

            }
            return MainStack.Pop();
        }
        /**************************************************************************/
        public async Task<Image> DrawMe()
        {
            var ep = ImmutableDictionary.CreateBuilder<Id, Id>();
            ep.Add("label", "ε");

            List<EdgeStatement> edges = new List<EdgeStatement>();
            GraphSet gpset = myGraph;
            foreach (Arista ar_it in gpset.aristas)
            {
                var label = ImmutableDictionary.CreateBuilder<Id, Id>();
                label.Add("label", ar_it.AristaValue);
                edges.Add(new EdgeStatement(ar_it.NodStart.ToString(), ar_it.NodEnd.ToString(), label.ToImmutable()));
            }

            Graph AnsGraph = Graph.Directed
            .Add(AttributeStatement.Graph.Set("rankdir", "LR"))
            .Add(AttributeStatement.Graph.Set("labelloc", "t"))
            .Add(AttributeStatement.Node.Set("style", "filled"))
            .Add(AttributeStatement.Node.Set("fillcolor", "#ECF0F1"))
            .Add(AttributeStatement.Graph.Set("label", "Graph for " + "Final Tree"))
            .AddRange(edges);
            ;
            await CreateImageFromGraph(AnsGraph);
            return CurrentImage;
        }

        private async Task CreateImageFromGraph(Graph graph)
        {
            IRenderer renderer = new Renderer(@"C:\Program Files (x86)\Graphviz2.38\bin");
            using (Stream file = File.Create("graph.png"))
            {
                await renderer.RunAsync(
                    graph, file,
                    RendererLayouts.Dot,
                    RendererFormats.Png,
                    CancellationToken.None);

                CurrentImage = Image.FromStream(file);
            }
        }
    }

}
