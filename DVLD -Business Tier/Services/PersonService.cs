using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class PersonService
    {
        
        public static int AddPerson(Person person)
        {
            int NewPersonID = -1;

            if (PersonRepository.IsPersonExist(person.NationalNO))
            {
                throw new Exception("Person is already exist");
            }

            if (!IsPersonInfoValid(person))
            {                
                throw new Exception("inValid person Information");
            }

            // Image Handling
            person.ImageName = SetImageProcess(person);
            if (string.IsNullOrEmpty(person.ImageName))
            {             
                throw new Exception("Error While Update Image");
            }

            try
            {
                NewPersonID = PersonRepository.AddNewPerson(person);
                return NewPersonID;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*** Error adding new person: " + ex.Message + "***");
                throw new Exception("Error While Add New Person");
            }
        }

        public static Person Find(int id)
        {
            Person person = PersonRepository.GetPersonByID(id);
            if (person == null)
            {
                throw new Exception("Person Not Found,Try Again");
            }
            return person;
        }

        public static List<Person> GetAll()
        {
            return PersonRepository.GetAllPeople();
        }

        public static bool Delete(int id)
        {
            if (!DeleteImage(id))
            {
                throw new Exception("Error While Delete Image");
            }

            if (!PersonRepository.DeletePerson(id))
            {
                throw new Exception("Error While Delete Person");
            }

            return true;
        }

        public static bool IsPersonExist(string nationalNO)
        {
            return PersonRepository.IsPersonExist(nationalNO);
        }

        public static bool Update(Person person)
        {
            bool isPersonUpdated = false;

            if (!IsPersonInfoValid(person))
            {
                throw new Exception("Person Information not valid");
            }

            // Image Handling            
            bool isPersonNOTUpdateImage = person.ImageName.Length == 40;
            if (!isPersonNOTUpdateImage)
            {
                person.ImageName = SetImageProcess(person);
                if (string.IsNullOrEmpty(person.ImageName))
                {
                    throw new Exception("Error While Update Image");
                }
            }

            try
            {
                PersonRepository.UpdatePerson(person);
                isPersonUpdated = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*** Error updating person: " + ex.Message + "***");
                throw new Exception("Error While Update Person");
            }

            return isPersonUpdated;
        }

        //Handeling Image Saving and Deletion
        private static string SetImageProcess(Person person)
        {
            
            if (string.IsNullOrEmpty(person.ImageName))
            {
                return string.Empty;
            }

            string fileExtension = Path.GetExtension(person.ImageName);
            string NewImageName = Guid.NewGuid().ToString() + fileExtension;

            string NewImageDestinationPath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", NewImageName);
            try
            {
                if (DeleteImage(person.PersonID))
                {
                    File.Copy(person.ImageName, NewImageDestinationPath, true);
                    return NewImageName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*** Error copying image: " + ex.Message + "***");
                throw new Exception("Error While Copying Image");
            }
            return string.Empty;
        }
        private static bool DeleteImage(int PersonId)
        {
            bool isDeleteImage = false;
            if (PersonId == -1 || PersonId == 0)
            {
                return true;
            }            

            Person person = Find(PersonId);
            if (person == null)
            {
                return false;                
            }

            string oldImageName = person.ImageName;
            if (string.IsNullOrEmpty(oldImageName))
            {
                return false;
            }

            string DeleteDestinationPath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", oldImageName);

            for (int attempts = 0; attempts < 3; attempts++)
            {
                try
                {
                    File.Delete($@"{DeleteDestinationPath}");
                    isDeleteImage = true;
                    break;
                }
                catch (IOException)
                {
                   
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    
                    System.Threading.Thread.Sleep(750);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("*** Error deleting image: " + ex.Message + "***");
                    return false;
                }
            }
            
            return isDeleteImage;
        }

        private static bool IsPersonInfoValid(Person person)
        {            
            if (string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.MiddelName)
                || string.IsNullOrEmpty(person.ThirdName)|| string.IsNullOrEmpty(person.LastName)
                ||string.IsNullOrEmpty(person.NationalNO)||string.IsNullOrEmpty(person.Email)||
                string.IsNullOrEmpty(person.Address) || string.IsNullOrEmpty(person.ImageName))
            {
                throw new Exception("Fill All the Feilds");
            }

            bool isAgeValid = DateTime.Now.Year - person.DateOfBirth.Year >= 18;
            if (!isAgeValid)
            {
                throw new Exception("Age Is Not inValid");
            }

            return true;
        }
    }
}
