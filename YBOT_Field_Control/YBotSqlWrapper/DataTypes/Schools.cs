using System;
using System.Linq;
using System.Collections.Generic;

namespace YBotSqlWrapper
{
    public class Schools : Generic<School>
    {
        public School this[int index] {
            get {
                foreach (var s in objectList) {
					if (index == s.id) {
						return s;
					}
				}
				return null;
            }
        }

        public School this[string name] {
            get {
                foreach (var s in objectList) {
                    if (name == s.name) {
                        return s;
                    }
                }
                return null;
            }
        }
    }

    public class School
    {
        public int id;
        public string name;

        public School (int id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}
