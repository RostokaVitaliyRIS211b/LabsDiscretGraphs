using Matrixs;
using System.Text;

namespace Graphs
{
    public static class ExInt
    {
        public static bool IsItPowOfTwo(this int num)
        {
            return (num & (num-1))==0;
        }
    }

    public class Graph
    {
        public virtual int this[string name1,string name2]
        {
            get
            {
                if (names.Count<2)
                    throw new Exception("Too few vertexes in Graph");
                if (!names.Contains(name1))
                    throw new ArgumentException($"Vertex {name1} is not exists in graph");
                if (!names.Contains(name2))
                    throw new ArgumentException($"Vertex {name2} is not exists in graph");
                return matrix[names.IndexOf(name1),names.IndexOf(name2)];
            }
            set
            {
                if (names.Count<2)
                    throw new Exception("Too few vertexes in Graph");
                if (!names.Contains(name1))
                    throw new ArgumentException($"Vertex {name1} is not exists in graph");
                if (!names.Contains(name2))
                    throw new ArgumentException($"Vertex {name2} is not exists in graph");
                if (value<0)
                    throw new ArgumentException("Value was below zero");
                matrix[names.IndexOf(name1), names.IndexOf(name2)]=value;
                if(!IsOriented)
                    matrix[names.IndexOf(name2), names.IndexOf(name1)]=value;
            }
        }
        public bool IsOriented = true;
        protected List<string> names = new();
        protected Matrix matrix = new();
        #region Constructors
        public Graph()
        {

        }
        public Graph(in Matrix matrix,in IEnumerable<string> names)
        {
            this.names = new(names);
            this.matrix=new Matrix(matrix);
        }
        public Graph(in Graph graph)
        {
            names = new(graph.names);
            matrix = new(graph.matrix);
        }
        #endregion
        public void AddVertex(in string name)
        {
            if (names.Contains(name))
                throw new ArgumentException("This name already in graph");
            names.Add(name);
            if (names.Count==1)
            {
                matrix.AddFrontString(new int[] { 0 });
            }
            else
            {
                List<int> ints = new(matrix.CountOfCollums);
                for(int i=0;i<matrix.CountOfCollums;++i)
                {
                    ints.Add(0);
                }
                matrix.AddLastString(ints);
                ints.Add(0);
                matrix.AddLastCollum(ints);
            }
        }
        public void DeleteVertex(in string name)
        {
            int index = names.IndexOf(name);
            if(index!=-1)
            {
                matrix.DeleteCollum(index);
                matrix.DeleteString(index);
                names.Remove(name);
            }
        }
        public IEnumerable<IEnumerable<string>> GetComponentsOfConnection()
        {
            List<List<string>> components = new();
            List<string> localNames = new(names);
            Matrix summ = GetMatrixOfReach();

            summ = summ.MultiplyByElem(summ.Transpose());

            while(summ.CountOfStrings!=0)
            {
                List<int> indexes = new();
                components.Add(new List<string>());
                for(int i=0;i<summ.CountOfCollums;++i)
                {
                    if (summ[0,i]>0)
                    {
                        indexes.Add(i);
                    }
                }
                int minus = 0;
                foreach(int i in indexes)
                {
                    components[components.Count-1].Add(localNames[i-minus]);
                    localNames.Remove(localNames[i-minus]);
                    summ.DeleteCollum(i-minus);
                    summ.DeleteString(i-minus);
                    ++minus;
                }
            }
            return components;
        }
        public IEnumerable<IEnumerable<string>> GetTieredParallelForm()
        {
            List<List<string>> components = new();
            Matrix copy = new(matrix);
            List<string> localNames = new(names);
            bool ZerosCollum(IEnumerable<int> ints)
            {
                bool flag = true;
                foreach(int i in ints)
                {
                    if(i!=0)
                    {
                        flag = false;
                        break;
                    }
                }
                return flag;
            }

            while (copy.CountOfStrings!=0)
            {
                List<int> indexes = new();
                components.Add(new List<string>());

                do
                {
                    indexes.Add(copy.IndexOfCollum(ZerosCollum));
                    if (indexes[indexes.Count-1]!=-1)
                    {
                        copy.DeleteCollum(indexes[indexes.Count-1]);
                    }
                } while (indexes[indexes.Count-1]!=-1);

                if (indexes[0]==-1)
                    throw new Exception("Could`t sort graph");

                for(int i=0;i<indexes.Count-1;++i)
                {
                    components[components.Count-1].Add(localNames[indexes[i]]);
                    localNames.Remove(localNames[indexes[i]]);
                    copy.DeleteString(indexes[i]);
                }
            }
            return components;
        }
        public IEnumerable<IEnumerable<string>> Deikstra(string name,out IEnumerable<int> weightOfWays)
        {

            if (!names.Contains(name))
                throw new ArgumentException($"This {name} vertex is not exists in graph");

            List<List<string>> ways = new();
            List<string> localNames = new(names);
            List<int> markOfVertex = new();
            List<bool> finalmarkOfVertexes = new();
            string currentVertex = name;

            localNames.Remove(name);

            foreach (string name1 in names)
            {
               
                if (name1!=name)
                {
                    markOfVertex.Add(int.MaxValue);
                    finalmarkOfVertexes.Add(false);
                }
                else
                {
                    markOfVertex.Add(0);
                    finalmarkOfVertexes.Add(true);
                }
            }

            while(localNames.Count>0)
            {
                List<string> closeVer = new(GetAdjustVertexes(currentVertex,false));
                int index1 = names.IndexOf(currentVertex);

                foreach(string name1 in closeVer)
                {
                    int index2 = names.IndexOf(name1),wayToVertex = matrix[index1, index2]+markOfVertex[index1];
                    if(localNames.Contains(name1) && (wayToVertex<markOfVertex[index2]))
                    {
                        markOfVertex[index2] = wayToVertex;
                    }
                }

                int minInd = -1,min = int.MaxValue;

                for(int i=0;i<markOfVertex.Count;++i)
                {
                    if (markOfVertex[i]<min && !finalmarkOfVertexes[i])
                    {
                        min = markOfVertex[i];
                        minInd = i;
                    }
                }

                if (minInd==-1)
                    throw new Exception("Minimum way is not exist");

                finalmarkOfVertexes[minInd] = true;
                localNames.Remove(names[minInd]);
                currentVertex = names[minInd];
            }

            List<string> GetWay(string currentName,string? prevName,string finalName)
            {
                List<string> namesOfVer = new();
                List<string> adjustVert = new(GetPrevVertexes(currentName));
                int index1 = names.IndexOf(currentName);

                if(currentName != finalName)
                {
                    foreach (string name in adjustVert)
                    {
                        int index2 = names.IndexOf(name);
                        if (name != (prevName ?? "") && markOfVertex[index1] >= markOfVertex[index2] + matrix[index2, index1])
                        {
                            List<string> buff = GetWay(name, currentName, finalName);
                            if(buff.Contains(finalName))
                            {
                                namesOfVer.AddRange(buff);
                                break;
                            }  
                        }
                    }
                    namesOfVer.Add(currentName);
                }
                else
                {
                    namesOfVer.Add(currentName);
                }

                if (prevName is null && !namesOfVer.Contains(finalName))
                    throw new Exception("Cannot find way");

                return namesOfVer;
            }

            foreach(string name1 in names)
            {
                if(name1!=name)
                {
                    List<string> s = GetWay(name1, null, name);
                    ways.Add(s);
                }
            }
            weightOfWays = markOfVertex;
            return ways;
        }
        public IEnumerable<IEnumerable<string>> ClicksOfGraph()
        {
            Matrix matrix1 = new(matrix);
            matrix1.ForAll((in1, in2, x) => x==0 ? 1 : 0);
            return new Graph(matrix1,names).GetInnerStabilitySets();
        }
        public IEnumerable<IEnumerable<string>> GetInnerStabilitySets()
        {
            List<List<string>> sets = new();
            List<List<int>> bytes = new List<List<int>>();
            #region ConstructDisjuncts
            for (int i=0;i<names.Count;++i)
            {
                for(int j=i+1;j<names.Count;++j)
                {
                    if (matrix[i,j]>0)
                    {
                        int num1 = (int)Math.Pow(2,i),num2 = (int)Math.Pow(2,j);
                        bytes.Add(new List<int>());
                        bytes[bytes.Count-1].Add(num1);
                        bytes[bytes.Count-1].Add(num2);
                    }
                }
            }
            #endregion
            #region ConvertToDNF
            while (bytes.Count!=1)
            {
                bytes.Add(new List<int>());
                foreach(int i in bytes[0])
                {
                    foreach(int j in bytes[1])
                    {
                        bytes[bytes.Count-1].Add(i|j);
                    }
                }
                bytes.RemoveAt(0);
                bytes.RemoveAt(0);
                List<int> currColl = bytes[bytes.Count-1];
                for (int i = 0;i < currColl.Count;++i)
                {
                    int currNum = currColl[i];
                    for (int j = 0; j < currColl.Count; ++j)
                    {
                        if ((currNum & currColl[j]) == currNum  &&  currNum != currColl[j])
                        {
                            currColl.RemoveAt(j);
                            i = j<i ? --i : i;
                            --j;
                        }
                    }
                }
            }
            #endregion
            #region InitSets
            foreach (int konjunct in bytes[0])
            {
                List<string> set = new();
                for(int i=0;i<names.Count;++i)
                {
                    int pow = (int)Math.Pow(2, i);
                    if ( (konjunct & pow) != pow )
                    {
                        set.Add(names[i]);
                    }
                }
                sets.Add(set);
            }
            #endregion
            return sets;
        }
        public Graph GetMinimunFrame()
        {
            if (!CanIGoEveryWhere())
                throw new Exception("This graph is not reachable");

            Graph ostov = new();

            ostov.IsOriented = IsOriented;

            IEnumerable<Edge> edges = GetEdges();


            foreach(Edge edge in edges)
            {
               
                ostov.AddEdge(edge);

                if(ostov.IsCyclExistsRec(-1,0,new List<int>()))
                {
                    ostov.DeleteEdge(edge);
                }

                if (ostov.GetNames().Count()==names.Count && ostov.CanIGoEveryWhere())
                    break;
            }

            return ostov;
        }
        public Matrix GetMatrixOfReach()
        {
            Matrix summ = new(matrix);
            summ.RaiseToZero();
            Matrix multiply = new(matrix);
            multiply.ForAll((in1, in2, x) => x>0 ? 1 : 0);
            Matrix matrixCop = new(matrix);
            matrixCop.ForAll((in1, in2, x) => x>0 ? 1 : 0);

            for (int i = 0; i<names.Count; ++i)
            {
                summ += multiply;
                multiply*=matrixCop;
            }
            return summ;
        }
        public bool CanIGoEveryWhere()
        {
            bool flag = true;
            Matrix matrixReach = GetMatrixOfReach();
            for (int i=0;i<names.Count && flag;++i)
            {
                for (int j = 0; j<names.Count && flag; ++j)
                {
                    flag = i==j || matrixReach[i, j]>0;
                }
            }
            return flag;
        }
        public bool IsCycleExists()
        {
            bool flag = false;
            Matrix matrixCop = new(matrix);
            Matrix multiply = new(matrix);
            multiply*=matrixCop;
            multiply*=matrixCop;
            for (int i=3;i<=names.Count && !flag;++i)
            {
                flag = multiply.Find( (index1, index2, value) => index1==index2 && value>0 );
                multiply*=matrixCop;
            }
            return flag;
        }
        public bool IsCyclExistsRec(int prevIndex,int currindex,List<int> usedVer)
        {
            bool flag = false;
            if(!usedVer.Contains(currindex))
            {
                usedVer.Add(currindex);
                for (int i = 0; i<names.Count && !flag; ++i)
                {
                    if (i!=prevIndex && i !=currindex &&  matrix[currindex, i]>0  &&  usedVer.Contains(i))
                    {
                        flag = true;
                    }
                    else if (i!=prevIndex && i !=currindex && matrix[currindex, i]>0)
                    {
                        flag = IsCyclExistsRec(currindex, i, usedVer);
                    }
                }
            }
            return flag;
        }
        public IEnumerable<string> GetNames()
        {
            return names.ToArray();
        }
        public Matrix GetMatrix()
        {
            return new Matrix(matrix);
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new(matrix.ToString());
            if (names.Count>0)
            {
                for (int i = names.Count-1; i>=1; --i)
                {
                    int counter = i;
                    for (int j = 0; j<stringBuilder.Length; ++j)
                    {
                        counter=stringBuilder[j]=='\n' ? --counter : counter;
                        if (counter==0)
                        {
                            stringBuilder.Insert(j+1, names[i] + " ");
                            break;
                        }
                    }
                }
                stringBuilder.Insert(0, names[0]+" ");
                for (int i = names.Count-1; i>=0; --i)
                {
                    stringBuilder.Insert(0, names[i]+" ");
                }
                stringBuilder.Insert(0, "  ");
                stringBuilder.Insert(names.Count*2+2, '\n');
            }
            else
            {
                stringBuilder = new("");
            }
           
            return stringBuilder.ToString();
        }
        public void SetMaxResultFunc(Func<int,int> func)
        {
            matrix.MaxResult=func;
        }
        public IEnumerable<string> GetAdjustVertexes(string name,bool allCon)
        {
            if (!names.Contains(name))
                throw new ArgumentException($"This {name} vertex is not exists in graph");
            List<string> nameses = new();
            int index1 = names.IndexOf(name);
            foreach (string name1 in names)
            {
                int index2 = names.IndexOf(name1);
                if (name1!=name && (matrix[index1, index2]+(allCon?matrix[index2, index1]:0)>0))
                {
                    nameses.Add(name1);
                }
            }
            return nameses;
        }
        public IEnumerable<string> GetPrevVertexes(string name)
        {
            if (!names.Contains(name))
                throw new ArgumentException($"This {name} vertex is not exists in graph");
            List<string> nameses = new();
            int index1 = names.IndexOf(name);
            foreach (string name1 in names)
            {
                int index2 = names.IndexOf(name1);
                if (name1!=name &&  matrix[index2, index1]>0)
                {
                    nameses.Add(name1);
                }
            }
            return nameses;
        }
        public IEnumerable<Edge> GetEdges()
        {
            List<Edge> edges = new();
            for(int i=0;i<names.Count;++i)
            {
                for(int j = IsOriented?0:i+1 ;j<names.Count;++j)
                {
                    if (matrix[i, j]>0)
                    {
                        edges.Add(new Edge(names[i], names[j], matrix[i, j]));
                    }
                }
            }
            edges.Sort((x, y) => x.Weight>y.Weight ? 1 : x.Weight==y.Weight ? 0 : -1);
            return edges;
        }
        public bool FindName(string name)
        {
            return names.Contains(name);
        }
        public void AddEdge(Edge edge)
        {
            if (!names.Contains(edge.NameOne))
                AddVertex(edge.NameOne);
            if (!names.Contains(edge.NameTwo))
                AddVertex(edge.NameTwo);
            this[edge.NameOne, edge.NameTwo] = edge.Weight;
            if(!IsOriented)
                this[edge.NameTwo, edge.NameOne] = edge.Weight;
        }
        public void DeleteEdge(Edge edge)
        {
            if (!names.Contains(edge.NameOne))
                throw new ArgumentException("This name not exists in graph");
            if (!names.Contains(edge.NameTwo))
                throw new ArgumentException("This name not exists in graph");
            this[edge.NameOne, edge.NameTwo] = 0;
            if (!IsOriented)
                this[edge.NameTwo, edge.NameOne] = 0;
        }
        public bool ContainsName(string name)
        {
            return names.Contains(name);
        }
        public void ChangeName(string oldName,string newName)
        {
            if (!names.Contains(oldName))
                throw new Exception("This name does not exists");
            names[names.IndexOf(oldName)]=newName;
        }
        public record Edge(string NameOne,string NameTwo,int Weight);
    }
}