using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FYProj
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    class relationshipPoint
    {
        public Point start, end;
        public GroupBox gb1, gb2;
        public string relType = "";

        public relationshipPoint(Point s, Point e, GroupBox g1, GroupBox g2, String type)
        {
            start = s;
            end = e;
            gb1 = g1;
            gb2 = g2;
            relType = type;
        }

        public Point[][] getGBPoints() {
            /*
            Point GB1_ULCorner = gb1.Location;
            Point GB1_URCorner = new Point(gb1.Location.X + gb1.Width, gb1.Location.Y);
            Point GB1_LLCorner = new Point(gb1.Location.X, gb1.Location.Y + gb1.Height);
            Point GB1_LRCorner = new Point(gb1.Location.X + gb1.Width, gb1.Location.Y + gb1.Height);

            Point GB2_ULCorner = gb2.Location;
            Point GB2_URCorner = new Point(gb2.Location.X + gb2.Width, gb2.Location.Y);
            Point GB2_LLCorner = new Point(gb2.Location.X, gb2.Location.Y + gb2.Height);
            Point GB2_LRCorner = new Point(gb2.Location.X + gb2.Width, gb2.Location.Y + gb2.Height);
            */

            Point[][] GroupBoxDimensions = new Point[2][];

            GroupBoxDimensions[0] = new Point[] {
                gb1.Location, //Top-Left Corner [0]
                new Point(gb1.Location.X + (gb1.Width / 2), gb1.Location.Y), //Top-Mid [1]

                new Point(gb1.Location.X + gb1.Width, gb1.Location.Y), //Top-Right Corner [2]
                new Point(gb1.Location.X + gb1.Width, gb1.Location.Y + (gb1.Height / 2)),//Right-Mid [3]

                new Point(gb1.Location.X + gb1.Width, gb1.Location.Y + gb1.Height), //Bottom-Right Corner [4]
                new Point(gb1.Location.X + (gb1.Width/2), gb1.Location.Y + gb1.Height), //Bottom-Mid [5]

                new Point(gb1.Location.X, gb1.Location.Y + gb1.Height), //Bottom-Left Corner [6]
                new Point(gb1.Location.X, gb1.Location.Y + (gb1.Height/2)) //Left-Mid [7]
            };

            GroupBoxDimensions[1] = new Point[] {
                gb2.Location, //Top-Left Corner
                new Point(gb2.Location.X + (gb2.Width / 2), gb2.Location.Y), //Top-Mid

                new Point(gb2.Location.X + gb2.Width, gb2.Location.Y), //Top-Right Corner
                new Point(gb2.Location.X + gb2.Width, gb2.Location.Y + (gb2.Height / 2)),//Right-Mid

                new Point(gb2.Location.X + gb2.Width, gb2.Location.Y + gb2.Height), //Bottom-Right Corner
                new Point(gb2.Location.X + (gb2.Width/2), gb2.Location.Y + gb2.Height), //Bottom-Mid

                new Point(gb2.Location.X, gb2.Location.Y + gb2.Height), //Bottom-Left Corner
                new Point(gb2.Location.X, gb2.Location.Y + (gb2.Height/2)) //Left-Mid
            };

            return GroupBoxDimensions;
        }
    }

    //Class to record ER Diagram
    public class ERModel {
        List<Entity> entities;
        List<Relationship> relationships;

        public ERModel() {
            entities = new List<Entity>();
            relationships = new List<Relationship>();
        }

        public void addEntity(Entity newEntity) 
        {
            entities.Add(newEntity);
        }

        public void removeEntity(Entity existingEntity)
        {
            entities.Remove(existingEntity);
        }

        public List<Entity> getEntities()
        {
            return entities;
        }

        public void addRelationship(Relationship newRelationship)
        {
            relationships.Add(newRelationship);
        }

        public void removeRelationship(Relationship existingRelationship)
        {
            relationships.Remove(existingRelationship);
        }

        public List<Relationship> getRelationships()
        {
            return relationships;
        }

        public Entity findEntity(string entityName) {
            foreach (Entity ent in entities) {
                if (ent.getName().Equals(entityName)) {
                    return ent;
                }
            }
            return null;
        }

        public Relationship findRelationship(string ent1, string ent2) {
            Entity entOne = findEntity(ent1);
            Entity entTwo = findEntity(ent2);

            if (entOne == null || entTwo == null)
            {
                return null;
            }

            Entity[] relatedEnts;

            foreach (Relationship rel in relationships) {
                relatedEnts = rel.relatedEntities();
                if ((relatedEnts[0] == entOne && relatedEnts[1] == entTwo) || (relatedEnts[1] == entOne && relatedEnts[0] == entTwo)) {
                    return rel;
                }
            }

            return null;
        }

        public void editEntityName(string oldName, string newName) {
            foreach (Entity ent in entities)
            {
                if (ent.getName().Equals(oldName))
                {
                    ent.rename(newName);
                    break;
                }
            }
        }
    }
    
    public class Entity {
        string name;
        List<EntityField> fields = new List<EntityField>();

        public Entity(string entityName) {
            name = entityName;
        }

        public void addField(EntityField newField) {
            fields.Add(newField);
        }

        public void removeField(string n)
        {
            foreach (EntityField entF in fields) {
                if (entF.getName() == n) {
                    fields.Remove(entF);
                    break;
                }
            }
            
        }

        public void editField(string oldName, string n, bool k)
        {
            foreach (EntityField entF in fields)
            {
                if (entF.getName() == oldName)
                {
                    entF.rename(n);
                    entF.changeKeyState(k);
                    break;
                }
            }
        }

        public bool fieldExists(string n)
        {
            foreach (EntityField entF in fields)
            {
                if (entF.getName() == n)
                {
                    return true;
                }
            }
            return false;
        }

        public List<EntityField> getFields()
        {
            return fields;
        }

        public void rename(string newName) {
            name = newName;
        }

        public string getName() {
            return name;
        }
    }

    public class EntityField {
        string name;
        bool key;

        public EntityField(string n, bool k) {
            name = n;
            key = k;
        }

        public string getName()
        {
            return name;
        }

        public void rename(string newName) {
            name = newName;
        }

        public void changeKeyState(bool newState) {
            key = newState;
        }
    }

    public class Relationship {
        string multiplicity;
        string participation;
        Entity entityOne;
        Entity entityTwo;

        public Relationship(string mult, string part, Entity ent1, Entity ent2) {
            multiplicity = mult;
            participation = part;
            entityOne = ent1;
            entityTwo = ent2;
        }

        public Entity[] relatedEntities() { 
            return new Entity[]{ entityOne, entityTwo };
        }
    }
}
