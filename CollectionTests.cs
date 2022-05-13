using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {

        [Test]
        public void Test_Empty_Collection()
        {
            //Arrange
            var nums = new Collection<int>();
            //Act
            //Assert
            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }


        [Test]
        public void Test_Collection_Single_Item()
        {
            //Arrange
            var nums = new Collection<int>(5);
            //Act
            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
        }


        [Test]
        public void Test_Collection_Multiple_Items()
        {
            //Arrange
            var nums = new Collection<int>(5, 6, 11);
            //Act
            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5, 6, 11]"));
        }


        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var nums = new Collection<int>();
            //Act
            nums.Add(6);
            nums.Add(11);
            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[6, 11]"));
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            //Arrange
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            //Act
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            //Assert
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //Arrange
            var name = new Collection<string>("Peter", "Maria");
            //Act
            var item0 = name[0];
            var item1 = name[1];
            //Assert
            Assert.That(item0,Is.EqualTo("Peter"));
            Assert.That(item1, Is.EqualTo("Maria"));
        }
        
        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            //Arrange
            var names = new Collection<string>("Bob", "Joe");
            //Act
            //Assert
            Assert.That(() => { var name = names[-1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));

        }
        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            //Arrange
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            //Act
            //Assert
            Assert.That(nestedToString, Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
          
        }
        [Test]
        public void Test_Collection_1MillionItems()
        {
            //Arrange
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1,itemsCount).ToArray());    
            //Act
            //Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for(int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
         }
        [Test]
        public void Test_Collection_InsertAtStart()
        {
            //Arrange
            var list1 = new Collection<int>();
            for (int i = 0; i < 10; i++)
                list1.Add(i);
            //Act
            list1.InsertAt(0, 99);
            //Assert
            Assert.That(list1[0] == 99);
        }
         [Test]
        public void Test_Collection_SetByIndex()
        {
            //Arrange
            Collection<int> nums = new Collection<int>();
            for (int i = 0; i < 10; i++)
                nums.Add(i);
            //Act
            nums[0] = -1;
            //Assert
            Assert.That(nums[0] == -1);
        }
        [Test]
        public void Test_Collection_Exchange()
        {
            //Arrange
            var nums = new Collection<int>(1, 5, 10);
            //Act
            nums.Exchange(0, nums.Count - 1);
            //Assert
            Assert.That(nums[0] == 10);
            Assert.That(nums[nums.Count-1] == 1);
        }
        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            //Arrange
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            //Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => collection.Exchange(-1, collection.Count + 1));
            //Assert
            Assert.IsTrue(exception.Message.Contains($"Parameter should be in the range [0...{collection.Count - 1}]"));
        }
        [Test]
        public void Test_Collection_Clear()
        {
            //Arrange
            Collection<string> collection = new Collection<string>(new string[] {"First", "Middle", "Last"});
            //Act
            collection.Clear();
            //Assert
            Assert.AreEqual(0, collection.Count);
        }
        [Test]
        public void Test_Collection_RemoveAt()
        {
            //Arrange
            Collection<int> collection = new Collection<int>(new int[] { 11, 15, 19 });
            //Act
            collection.RemoveAt(1);
            //Assert
            Assert.That(collection[1] == 19);
         }
    }
}