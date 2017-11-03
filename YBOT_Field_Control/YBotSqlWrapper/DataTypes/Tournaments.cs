using System;

namespace YBotSqlWrapper
{
    public class Tournaments : Generic<Tournament>
    {
        public Tournament this[int index] {
            get {
                return objectList[index];
            }
        }

        public Tournament this[string name] {
            get {
                foreach (var t in objectList) {
                    if (name == t.name) {
                        return t;
                    }
                }
                return null;
            }
        }
    }

    public class Tournament
    {
        public int id;
        public DateTime date;
        public string name;

        public Tournament (int id, DateTime date, string name) {
            this.id = id;
            this.date = date;
            this.name = name;
        }
    }
}
