

using System.Text;

namespace Matrixs
{
    public class Matrix:ICloneable
    {
        public int this[int index1, int index2]
        {
            get 
            {
                if (matr.Count==0)
                    throw new Exception("Matrix was Empty");
                if (index1<0 || index1>=matr.Count)
                    throw new ArgumentException("Index1 was out of bounds");
                if(index2<0 || index2>=matr[0].Count)
                    throw new ArgumentException("Index2 was out of bounds");
                return matr[index1][index2];
            }
            set
            {
                if (matr.Count==0)
                    throw new Exception("Matrix was Empty");
                if (index1<0 || index1>=matr.Count)
                    throw new ArgumentException("Index1 was out of bounds");
                if (index2<0 || index2>=matr[0].Count)
                    throw new ArgumentException("Index2 was out of bounds");
                matr[index1][index2]=MaxResult(value);
            }
        }
        public int CountOfStrings { get => matr.Count; }
        public int CountOfCollums 
        {
            get
            {
                if (matr.Count==0)
                    throw new Exception("Matrix was empty");
                return matr[0].Count;
            }
        }
        protected List<List<int>> matr = new();
        public Func<int, int> MaxResult = x => x;
        public Matrix()
        {
            matr = new();
        }
        public Matrix(in IEnumerable<IEnumerable<int>> matr)
        {
            this.matr=new(matr.Count());
            foreach(IEnumerable<int> ints in matr)
            {
                this.matr.Add(new List<int>(ints));
            }
        }
        public Matrix(in Matrix matrix)
        {
            matr=new(matrix.matr.Count);
            foreach (IEnumerable<int> ints in matrix.matr)
            {
                matr.Add(new List<int>(ints));
            }
        }
        #region Adds
        public void AddLastString(IEnumerable<int> ints)
        {
            if (matr.Count!=0 &&  ints.Count()!=matr[0].Count)
                throw new Exception("String size is non appropriate");
            matr.Add(new List<int>(ints));
        }
        public void AddLastCollum(IEnumerable<int> ints)
        {
            if (matr.Count!=0 && ints.Count()!=matr.Count )
                throw new Exception("Collum size is non appropriate");
            IEnumerator<int> enumerator = ints.GetEnumerator();
            enumerator.MoveNext();
            if (matr.Count==0)
            {
                for (int i = 0; i<ints.Count(); ++i)
                {
                    matr.Add(new List<int>(1));
                }
            }
            for (int i=0;i<matr.Count;++i)
            {
                matr[i].Add(enumerator.Current);
                enumerator.MoveNext();
            }
        }
        public void AddFrontString(IEnumerable<int> ints)
        {
            if (matr.Count!=0 && ints.Count()!=matr[0].Count)
                throw new Exception("String size is non appropriate");
            matr.Insert(0, new List<int>(ints));
        }
        public void AddFrontCollum(IEnumerable<int> ints)
        {
            if (ints.Count()!=matr.Count && matr.Count!=0)
                throw new Exception("Collum size is non appropriate");
            IEnumerator<int> enumerator = ints.GetEnumerator();
            enumerator.MoveNext();
            if (matr.Count==0)
            {
                for (int i = 0; i<ints.Count(); ++i)
                {
                    matr.Add(new List<int>(1));
                }
            }
            for (int i = 0; i<matr.Count; ++i)
            {
                matr[i].Insert(0,enumerator.Current);
                enumerator.MoveNext();
            }
        }
        public void AddString(int index,IEnumerable<int> ints)
        {
            if (index<0 || index>=matr.Count && matr.Count!=0)
                throw new ArgumentException("Index was out of bounds");
            if (matr.Count!=0 && ints.Count()!=matr[0].Count )
                throw new Exception("String size is non appropriate");
            index = matr.Count==0 ? 0 : index;
            matr.Insert(index,new List<int>(ints));
        }
        public void AddCollum(int index,IEnumerable<int> ints)
        {
            if (index<0 || matr.Count!=0 && index>=matr[0].Count)
                throw new ArgumentException("Index2 was out of bounds");
            if (matr.Count!=0 && ints.Count()!=matr.Count)
                throw new Exception("Collum size is non appropriate");
            IEnumerator<int> enumerator = ints.GetEnumerator();
            enumerator.MoveNext();
            if(matr.Count==0)
            {
                index = 0;
                for(int i=0;i<ints.Count();++i)
                {
                    matr.Add(new List<int>(1));
                }
            }
            for (int i = 0; i<matr.Count; ++i)
            {
                matr[i].Insert(index,enumerator.Current);
                enumerator.MoveNext();
            }
        }
        #endregion
        public void DeleteString(int index)
        {
            if (index<0 || index>=matr.Count)
                throw new ArgumentException("Index was out of range");
            matr.RemoveAt(index);
        }
        public void DeleteCollum(int index)
        {
            if (matr.Count==0)
                throw new Exception("Matrix was empty");
            if (index<0 || index>=matr[0].Count)
                throw new ArgumentException("Index was out of range");
            foreach(List<int> list in matr)
            {
                list.RemoveAt(index);
            }
        }
        public bool ContainsItem(int item)
        {
            bool flag = false;
            for (int i = 0; i<matr.Count && !flag; ++i)
            {
                flag = matr[i].Contains(item);
            }
            return flag;
        }
        #region Indexers
        public int IndexOfItemString(int item)
        {
            int flag = -1;
            for(int i=0;i<matr.Count && flag==-1;++i)
            {
                flag = matr[i].Contains(item)?i:-1;
            }
            return flag;
        }
        public int IndexOfItemCollum(int item)
        {
            int flag = -1;
            for (int i = 0; i<matr.Count && flag==-1; ++i)
            {
                flag = matr[i].Contains(item) ? matr[i].IndexOf(item) : -1;
            }
            return flag;
        }
        public int IndexOfString(Predicate<IEnumerable<int>> predicate)
        {
            int index = -1;
            for(int i=0;i<matr.Count && index==-1;++i)
            {
                index = predicate(matr[i]) ? i : -1;
            }
            return index;
        }
        public int IndexOfCollum(Predicate<IEnumerable<int>> predicate)
        {
            int index = -1;
            for (int i = 0; i<CountOfCollums && index==-1; ++i)
            { 
                index = predicate(GetCollum(i)) ? i : -1;
            }
            return index;
        }
        #endregion
        public Matrix Transpose()
        {
            List<List<int>> list = new();
            for(int i=0;i<CountOfCollums;++i)
            {
                list.Add(new List<int>(GetCollum(i)));
            }
            return new Matrix(list);
        }
        public void Clear()
        {
            matr = new();
        }
        public object Clone()
        {
            return new Matrix(matr);
        }
        public static Matrix operator *(in Matrix matrix1,in Matrix matrix2)
        {
            if (matrix1.CountOfCollums!=matrix2.CountOfStrings)
                throw new Exception("Non-conformable matrices in operator *");
            List<List<int>> matrix = new(matrix1.CountOfStrings);
            for (int i=0;i<matrix1.CountOfStrings;++i)
            {
                matrix.Add(new List<int>());
                for(int j=0;j<matrix2.CountOfCollums;++j)
                {
                    matrix[i].Add(0);
                    for (int k = 0; k < matrix1.CountOfCollums; ++k)
                    {
                        matrix[i][j] = matrix1.MaxResult(matrix[i][j] + matrix1[i, k] * matrix2[k, j]);
                    }
                }
            }
            var matr = new Matrix(matrix);
            matr.MaxResult=matrix1.MaxResult;
            return matr;
        }
        public static Matrix operator +(in Matrix matrix1, in Matrix matrix2)
        {
            if (matrix1.CountOfStrings!=matrix2.CountOfStrings || matrix1.CountOfCollums!=matrix2.CountOfCollums)
                throw new Exception("Non-conformable matrices in operator +");
            List<List<int>> matrix = new(matrix1.CountOfStrings);
            for (int i = 0; i<matrix1.CountOfStrings; ++i)
            {
                matrix.Add(new List<int>(matrix1.CountOfCollums));
                for (int j = 0; j<matrix1.CountOfCollums; ++j)
                {
                    matrix[i].Add(matrix1.MaxResult(matrix1[i, j]+matrix2[i, j]));
                }
            }
            var matr = new Matrix(matrix)
            {
                MaxResult=matrix1.MaxResult
            };
            return matr;
        }
        public void RaiseToZero()
        {
            for(int i=0;i<CountOfStrings;++i)
            {
                for(int j=0;j<CountOfCollums;++j)
                {
                    matr[i][j]=i==j ? 1 : 0;
                }
            }
        }
        public void ForAll(Func<int,int,int,int> func)
        {
            for(int i=0;i<CountOfStrings;++i)
            {
                for(int j=0;j<CountOfCollums;++j)
                {
                    matr[i][j] = func(i, j, matr[i][j]);
                }
            }
        }
        public Matrix MultiplyByElem(Matrix matrix)
        {
            if (matrix.CountOfCollums!=CountOfCollums || matrix.CountOfStrings!=CountOfStrings)
                throw new Exception("For elem multiply need equal size matrixs");
            List<List<int>> ints = new();
            for(int i=0;i<CountOfStrings;++i)
            {
                ints.Add(new List<int>());
                for(int j=0;j<CountOfCollums;++j)
                {
                    ints[i].Add(MaxResult(matrix[i, j]*this[i, j]));
                }
            }
            var matr = new Matrix(ints);
            matr.MaxResult=matrix.MaxResult;
            return matr;
        }
        public int[,]? GetMatrixMassive()
        {
            int[,]? matrix = matr.Count>0 ? new int[matr.Count, matr[0].Count] : null;
            if(matr.Count!=0 && matrix is not null)
            {
                for(int i=0;i<matr.Count;++i)
                {
                    for(int j = 0; j<matr[0].Count;++j)
                    {
                        matrix[i, j]=matr[i][j];
                    }
                }
            }
            return matrix;
        }
        public int[] GetString(int index)
        {
            if (index<0 || index>=matr.Count)
                throw new ArgumentException("Index was out of bounds");
            return matr[index].ToArray();
        }
        public int[] GetCollum(int index)
        {
            if (matr.Count==0)
                throw new Exception("Matrix was empty");
            if (index<0 || index>=matr[0].Count)
                throw new ArgumentException("Index2 was out of bounds");
            List<int> collum = new();
            for (int j = 0; j<CountOfStrings; ++j)
            {
                collum.Add(matr[j][index]);
            }
            return collum.ToArray();
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            foreach(List<int> ints in matr)
            {
                foreach(int i in ints)
                {
                    stringBuilder.Append(i + " ");
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }
        public bool Find(Func<int,int,int,bool> func)
        {
            bool flag = false;
            for(int i=0;i<CountOfStrings && !flag;++i)
            {
                for(int j=0;j<CountOfCollums && !flag;++j)
                {
                    flag = func(i, j, matr[i][j]);
                }
            }
            return flag;
        }
        public int CountValueInString(int index,Predicate<int> pr)
        {
            if (index<0||index>=CountOfStrings)
                throw new ArgumentException("Index was out of range");
            return matr[index].FindAll(pr).Count;
        }
    }
}
