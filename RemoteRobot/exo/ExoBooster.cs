namespace RemoteRobot.exo
{
    [Serializable]
    public class ExoBooster
    {
        private static readonly long serialVersionUID = 2L;
        private string lander;
        private Position position;
        private string userData;
        private List<BoosteRobotPart> partList;

        public ExoBooster(string lander, Position position, string userData, string className, byte[] classData)
        {
            this.lander = lander;
            this.position = position;
            this.userData = userData;
            partList = new List<BoosteRobotPart>();
            partList.Add(new BoosteRobotPart(className, classData));
        }

        public void AddPart(string className, byte[] classData)
        {
            partList.Insert(0, new BoosteRobotPart(className, classData));
        }

        public string GetLander()
        {
            return lander;
        }

        public Position GetPosition()
        {
            return position;
        }

        public string GetUserData()
        {
            return userData;
        }

        public int GetPartCount()
        {
            return partList.Count;
        }

        public string GetClassName(int index)
        {
            return index >= 0 && index < partList.Count ? partList[index].GetClassName() : null;
        }

        public byte[] GetClassData(int index)
        {
            return index >= 0 && index < partList.Count ? partList[index].GetClassData() : null;
        }

        [Serializable]
        private class BoosteRobotPart(string className, byte[] classData)
        {
            private string className = className;
            private byte[] classData = classData;

            public string GetClassName()
            {
                return className;
            }

            public byte[] GetClassData()
            {
                return classData;
            }
        }
    }
}