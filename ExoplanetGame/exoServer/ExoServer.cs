using Exoplanet.exo;
using Exoplanet.exoServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExoServer
{
    public class ExoPlanet : Planet
    {
        private const int OP_LAND = 0;
        private const int OP_MOVE = 1;
        private const int OP_ROTATE = 2;
        private const int OP_SCAN = 3;
        private const int OP_CHARGE = 4;

        private string name = "Default-Planet";
        protected bool advanced;
        private bool expert;
        private float adjustFactor = 4.0F;
        private bool noDelay;

        private static readonly float[][] TempTable = new float[][]
        {
            new float[] { -100.0F, 50.0F },
            new float[] { -50.0F, 50.0F },
            new float[] { -20.0F, 30.0F },
            new float[] { 0.0F, 20.0F },
            new float[] { 20.0F, 19.0F },
            new float[] { 50.0F, 30.0F },
            new float[] { 100.0F, 50.0F },
            new float[] { 400.0F, 300.0F },
            new float[] { 800.0F, 400.0F },
            new float[] { 1100.0F, 300.0F }
        };

        private static readonly float[] TempGround = new float[] { -999.9F, 15.0F, 17.0F, 19.0F, 10.0F, 20.0F, 10.0F, 800.0F };
        private ExoMeasure[][] topo;
        private Size size;
        private Dictionary<Robot, ExoRobotStatus> robPos;
        private RobotProfil standardProfil;

        public Measure[][] Topo => topo;

        public string Name => name;

        public void SetNoDelay(bool noDelay)
        {
            this.noDelay = noDelay;
        }

        public ExoPlanet(int level, bool noDelay)
        {
            string[] topoStr = { "GSS3PFSGGL", "SP34PSFFLL", "SG3PMSFLLF", "SS23MGSFFG", "FGS24SSGFG", "FF33GGSGFF" };
            string[] tempStr = { "4444555667", "4444456788", "4445457987", "3444356766", "1233445555", "0124445444" };
            InitFromString(topoStr, tempStr, 0.0F);
            robPos = new Dictionary<Robot, ExoRobotStatus>();
            advanced = level > 0;
            expert = level == 2;
            standardProfil = new RobotProfil();
            this.noDelay = noDelay;
        }

        public ExoPlanet(int level, string filename, bool fromText, bool noDelay)
        {
            Console.WriteLine("filename=" + filename);
            if (!fromText)
                LoadTopoObj(filename);
            else
                LoadTopoFromTextfile(filename);

            robPos = new Dictionary<Robot, ExoRobotStatus>();
            advanced = level > 0;
            expert = level == 2;
            this.noDelay = noDelay;
        }

        //TODO: Change obsolete Method to JSON-Serializer
        [Obsolete("Obsolete")]
        public void SaveTopoObj(string filename)
        {
            try
            {
                using FileStream fileStream = new(filename, FileMode.Create);
                BinaryFormatter binaryFormatter = new();
                binaryFormatter.Serialize(fileStream, name);
                binaryFormatter.Serialize(fileStream, size);
                binaryFormatter.Serialize(fileStream, topo);
                binaryFormatter.Serialize(fileStream, standardProfil);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        //TODO: Change obsolete Method to JSON-Serializer
        [Obsolete("Obsolete")]
        public bool LoadTopoObj(string filename)
        {
            bool rc = false;

            try
            {
                using (FileStream fileStream = new(filename, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new();
                    object o = binaryFormatter.Deserialize(fileStream);

                    if (o is string s)
                    {
                        name = s;
                        o = binaryFormatter.Deserialize(fileStream);
                        if (o is Size size1)
                        {
                            size = size1;
                            topo = (ExoMeasure[][])binaryFormatter.Deserialize(fileStream);
                            standardProfil = (RobotProfil)binaryFormatter.Deserialize(fileStream);
                            rc = true;
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return rc;
        }

        public bool LoadTopoFromTextfile(string filename)
        {
            bool rc = false;

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    name = reader.ReadLine();
                    Size size = Size.Parse(reader.ReadLine());
                    string[] topoStr = new string[size.Height];

                    for (int i = 0; i < size.Height; ++i)
                    {
                        topoStr[i] = reader.ReadLine();
                    }

                    string temp = reader.ReadLine();
                    string[] tempStr = null;
                    float offset = 0.0F;

                    if (temp != null)
                    {
                        offset = float.Parse(temp);
                        tempStr = reader.ReadLine().Split(' ');
                    }

                    InitFromString(topoStr, tempStr, offset);
                    rc = true;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return rc;
        }

        private void InitFromString(string[] topoStr, string[] tempStr, float offset)
        {
            size = new Size(topoStr[0].Length, topoStr.Length);
            topo = new ExoMeasure[size.Height][];

            for (int i = 0; i < size.Height; ++i)
            {
                topo[i] = new ExoMeasure[size.Width];
                char[] chars = topoStr[i].ToCharArray();

                for (int j = 0; j < size.Width; ++j)
                {
                    char c = chars[j];
                    int index = c - '0';
                    topo[i][j] = new ExoMeasure(TempGround[index], TempTable[index][0], TempTable[index][1]);
                }
            }

            if (tempStr != null)
            {
                for (int i = 0; i < size.Height; ++i)
                {
                    char[] chars = tempStr[i].ToCharArray();

                    for (int j = 0; j < size.Width; ++j)
                    {
                        char c = chars[j];
                        int index = c - '0';
                        topo[i][j].Add(TempGround[index], TempTable[index][0], TempTable[index][1]);
                    }
                }
            }

            for (int i = 0; i < size.Height; ++i)
            {
                for (int j = 0; j < size.Width; ++j)
                {
                    if (topo[i][j].Roughness > 0)
                    {
                        topo[i][j].Add(0, topo[i][j].Temperature, topo[i][j].Roughness);
                    }
                }
            }
        }

        public string Execute(Robot robot, string cmd)
        {
            ExoRobotStatus status = null;

            if (robPos.ContainsKey(robot))
            {
                status = robPos[robot];
            }
            else
            {
                status = new ExoRobotStatus(robot, size.Width, size.Height);
                robPos.Add(robot, status);
            }

            return Execute(status, cmd);
        }

        private string Execute(ExoRobotStatus status, string cmd)
        {
            string rc = "OK";
            int op = cmd[0] - '0';

            switch (op)
            {
                case OP_LAND:
                    status.Land(this);
                    break;

                case OP_MOVE:
                    status.Move(this);
                    break;

                case OP_ROTATE:
                    status.Rotate();
                    break;

                case OP_SCAN:
                    rc = status.Scan(this);
                    break;

                case OP_CHARGE:
                    status.Charge();
                    break;

                default:
                    rc = "Error: Invalid operation code.";
                    break;
            }

            return rc;
        }

        public Measure Land(Robot robot, Position position)
        {
            throw new NotImplementedException();
        }

        public Position GetPosition(Robot robot)
        {
            throw new NotImplementedException();
        }

        public Position Move(Robot robot)
        {
            throw new NotImplementedException();
        }

        public Direction Rotate(Robot robot, Rotation rotation)
        {
            throw new NotImplementedException();
        }

        public Measure Scan(Robot robot)
        {
            throw new NotImplementedException();
        }

        public Size GetSize()
        {
            throw new NotImplementedException();
        }

        public void Remove(Robot robot)
        {
            throw new NotImplementedException();
        }

        public RobotStatus Charge(Robot robot, int value)
        {
            throw new NotImplementedException();
        }
    }
}