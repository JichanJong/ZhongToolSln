using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class DynamicModel : DynamicObject
    {
        private string propertyName;
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        // The inner dictionary.
        Dictionary<string, object> dicProperty
            = new Dictionary<string, object>();
        public Dictionary<string, object> DicProperty
        {
            get
            {
                return dicProperty;
            }
        }


        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dicProperty.Count;
            }
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name;

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dicProperty.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            if (binder.Name == "Property")
            {
                dicProperty[PropertyName] = value;
            }
            else
            {
                dicProperty[binder.Name] = value;
            }


            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("动态为类型添加属性");
            dynamic dynamicModel = new DynamicModel();
            List<string> myList = new List<string>();
            myList.Add("Name");
            myList.Add("Age");
            myList.Add("Hobby");

            List<string> myValueList = new List<string>();
            myValueList.Add("Mary");
            myValueList.Add("18");
            myValueList.Add("Dance");
            dynamicModel. = "fadsfadsfasd";
            dynamicModel.Property = "fdsafdsafsad";


            //for (int i = 0; i < myList.Count; i++)
            //{
            //    string myAttr = myList[i];
            //    dynamicModel.PropertyName = myAttr;
            //    dynamicModel.Property = myValueList[i];
            //}

            Console.WriteLine($"Name: {dynamicModel.Name}");
            Console.WriteLine($"Age: {dynamicModel.Age}");
            Console.WriteLine($"Hobby: {dynamicModel.Hobby}");
        }
    }
}
