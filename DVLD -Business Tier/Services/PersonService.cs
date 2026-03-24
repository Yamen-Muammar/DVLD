using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class PersonService
    {
        private PersonRepository _personRepo;
        public PersonService()
        {
            _personRepo = new PersonRepository();
        }
        // CRUD Operations
        public async Task<int> AddPerson(Person person)
        {
            int NewPersonID = -1;

            if (await _personRepo.IsPersonExist(person.NationalNO))
            {
                throw new Exception("Person is already exist");
            }

            if (!IsPersonInfoValid(person))
            {
                throw new Exception("inValid person Information");
            }

            // Image Handling
            person.ImageName = await _setImageProcess(person);
            if (string.IsNullOrEmpty(person.ImageName))
            {
                throw new Exception("Error While Update Image");
            }

            NewPersonID = await _personRepo.AddNewPerson(person);
            if (NewPersonID == -1)
            {
                throw new Exception("Can not Add the Person");
            }
            return NewPersonID;
        }

        public async Task<Person> Find(int id)
        {
            if (id == 0)
            {
                throw new Exception("No Data Passed");
            }

            Person person = await _personRepo.GetPersonByID(id);

            if (person == null)
            {
                throw new Exception("Person Not Found");
            }
            return person;
        }

        public async Task<Person> Find(string nationalNO)
        {
            if (string.IsNullOrEmpty(nationalNO))
            {
                throw new ArgumentNullException();
            }
            Person person = await _personRepo.GetPersonByNationalNO(nationalNO);

            if (person == null)
            {
                throw new Exception("Person Not Found");
            }
            return person;
        }

        public async Task<List<clsPersonView>> GetAll()
        {
            List<clsPersonView> PeopleList = new List<clsPersonView>();
            PeopleList = await _personRepo.GetAllPeople();
            return PeopleList;
        }

        public async Task<bool> Delete(int id)
        {
            bool isPersonDeleted = false;
            if (!await _deleteImage(id))
            {
                throw new Exception("Error While Delete Image");
            }

            isPersonDeleted = await _personRepo.DeletePerson(id);
            if (!isPersonDeleted)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsPersonExist(string nationalNO)
        {
            bool isPersonExist = false;

            isPersonExist = await _personRepo.IsPersonExist(nationalNO);

            return isPersonExist;
        }

        public async Task<bool> Update(Person person)
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
                person.ImageName = await _setImageProcess(person);
                if (string.IsNullOrEmpty(person.ImageName))
                {
                    throw new Exception("Error While Update Image");
                }
            }
            if (await _personRepo.UpdatePerson(person))
            {
                isPersonUpdated = true;
            }


            return isPersonUpdated;
        }

        //Handeling Image Saving and Deletion
        private async Task<string> _setImageProcess(Person person)
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
                if (await _deleteImage(person.PersonID))
                {
                    File.Copy(person.ImageName, NewImageDestinationPath, true);
                    return NewImageName;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return string.Empty;
        }
        private async Task<bool> _deleteImage(int PersonId)
        {
            bool isDeleteImage = false;
            if (PersonId == -1 || PersonId == 0)
            {
                return true;
            }

            Person person = await Find(PersonId);
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

        // Information Validation
        private static bool IsPersonInfoValid(Person person)
        {
            if (string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.MiddelName)
                || string.IsNullOrEmpty(person.ThirdName) || string.IsNullOrEmpty(person.LastName)
                || string.IsNullOrEmpty(person.NationalNO) || string.IsNullOrEmpty(person.Email) ||
                string.IsNullOrEmpty(person.Address) || string.IsNullOrEmpty(person.ImageName))
            {
                throw new ArgumentException("Fill All the Feilds");
            }

            bool isAgeValid = DateTime.Now.Year - person.DateOfBirth.Year >= 18;
            if (!isAgeValid)
            {
                throw new ArgumentException("Age Is Not inValid");
            }

            return true;
        }
    }
}
