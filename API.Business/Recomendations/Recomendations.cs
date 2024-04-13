using API.Business.Objects;
using API.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Business.Recomendations
{
    public class Recomendations
    {
        public int[,] matrix;

        public Recomendations(User user) 
        {
            matrix = new int[Place.allPlaces.Count, 0];

            foreach(var i in User.users)
            {
                if (i.reactions.Count > 50)
                {
                    int[] newColumn = new int[Place.allPlaces.Count];

                    foreach(var place in i.likes)
                    {
                        newColumn[place] = 1;
                    }

                    foreach(var place in i.dislikes)
                    {
                        newColumn[place] = -1;
                    }

                    matrix = AddColumnToArray(matrix, newColumn);
                }
            }
        }

        public Place[] GetTenRecomendations(User user)
        {
            Recomendations recomendations = new Recomendations(user);
            List<Place> places = new List<Place>();
            
            //смотрим количество лайкнутых, если больше 9, то выбираем случайные 9 и у них ищем ближайшего неоцененного соседа один берем случайным образом
            if(user.likes.Count >= 9)
            {
                //перебираем 9 лайкнутых
                for(int i = 0; i<9; i++)
                {
                    int random = new Random().Next(0, user.likes.Count);

                    double[,] matxOrRecomendations = recomendations.GetCos(matrix, Place.allPlaces[user.likes[i]]);

                    //находим самое близкое к лайкнутому из непросмотренных
                    Place placeForUser = GetOneRecomendation(user, Place.allPlaces[user.likes[i]], matxOrRecomendations);
                    try
                    {
                        places.Add(placeForUser);
                    }
                    catch(Exception ex) { }
                }
                try
                {
                    //добавлем в набор одно случайное из непросмотренных
                    places.Add(GetOneNew(Place.allPlaces, user.reactions));
                }
                catch(Exception ex) { }

                return places.ToArray();
            }
            else   //если лакнутых меньше то берем ближайших соседей у этих лайкнутых остальные места случайным образом выбираем
            {
                foreach(int place in user.likes)
                {
                    double[,] matxOrRecomendations = recomendations.GetCos(matrix, Place.allPlaces[place]);

                    Place placeForUser = GetOneRecomendation(user, Place.allPlaces[place], matxOrRecomendations);
                    try
                    {
                        places.Add(placeForUser);
                    }
                    catch (Exception ex) { }
                }

                //добираем набор непросмотренными 
                for(int i = 0; i < 10-places.Count; i++)
                {
                    try
                    {
                        places.Add(GetOneNew(Place.allPlaces, user.reactions));
                    }
                    catch (Exception ex) { }
                }
                return places.ToArray();
            }
        }

        //дает одну рекомендацию пользователю к конкретному месту
        public static Place GetOneRecomendation(User user, Place currentPlace, double[,] matrix)
        {
            for(int i = 0; i< matrix.GetLength(0); i++)
            {
                if (!user.reactions.Contains((int)matrix[i, 0]))
                {
                    return Place.allPlaces[(int)matrix[i, 0]];
                }
            }
            return null;
        }

        //дает одно ранее непросмотренное случайно
        public static Place GetOneNew(List<Place> places, List<int> reactions)
        {
            Random random = new Random();
            List<int> unreacted = new List<int>();
            foreach(Place place in places)
            {
                if (!reactions.Contains(int.Parse(place.Id)))
                    unreacted.Add(int.Parse(place.Id));
            }

            if (unreacted.Count > 0)
            {
                return Place.allPlaces[unreacted[random.Next(0, unreacted.Count)]];
            }
            else
            {
                return null;
            }
        }

        public double[,] GetCos(int[,] matrix, Place place)
        {
            double[,] matrixOfDistance = new double[Place.allPlaces.Count - 1, 2];

            for (int i = 0; i <int.Parse(place.Id); i++)
            {
                double sum = 0;
                
                for(int j = 0; j< matrix.GetLength(1) ; j++)
                {
                    sum+= Math.Pow(matrix[i,j] - matrix[int.Parse(place.Id), j], 2);
                }

                double distance = Math.Sqrt(sum);

                matrixOfDistance[i, 0] = i;
                matrixOfDistance[i, 1] = distance;
            }
            for (int i = int.Parse(place.Id); i < matrix.GetLength(0); i++)
            {
                double sum = 0;

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sum += Math.Pow(matrix[i, j] - matrix[int.Parse(place.Id), j], 2);
                }

                double distance = Math.Sqrt(sum);

                matrixOfDistance[i, 0] = i+1;
                matrixOfDistance[i, 1] = distance;
            }

            return matrixOfDistance;
        }

        //функцию сортировки этой матрицы по близости
        static double[,] SortMatrixBySecondColumn(double[,] matrix)
        {
            // Сортировка матрицы по второму столбцу
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, 1] > matrix[j, 1])
                    {
                        // Обмен значений элементов
                        double tempId = matrix[i, 0];
                        double tempValue = matrix[i, 1];
                        matrix[i, 0] = matrix[j, 0];
                        matrix[i, 1] = matrix[j, 1];
                        matrix[j, 0] = tempId;
                        matrix[j, 1] = tempValue;
                    }
                }
            }
            return matrix;
        }

        //добавления столбца с оценками отдельного пользователя в матрицу
        public int[,] AddColumnToArray(int[,] originalArray, int[] newColumn)
        {
            // Получаем размеры исходного массива
            int rows = originalArray.GetLength(0);
            int cols = originalArray.GetLength(1);

            // Создаем новый массив с дополнительным столбцом
            int[,] newArray = new int[rows, cols + 1];

            // Копируем данные из исходного массива в новый
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newArray[i, j] = originalArray[i, j];
                }
            }

            // Добавляем новый столбец
            for (int i = 0; i < rows; i++)
            {
                newArray[i, cols] = newColumn[i];
            }

            return newArray;
        }

    }
}
