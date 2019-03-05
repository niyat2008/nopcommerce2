using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Microsoft.AspNetCore.Hosting;


namespace Nop.Services.Z_ConsultantAdmin.Customers
{
    public class CustomersService : ICustomerService
    {
        #region Fields
        private readonly IRepository<Customer> _customerRepository;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        #endregion
        #region Ctor
        public CustomersService(IRepository<Customer> customerService, Core.IWorkContext workContext, IHostingEnvironment env)
        {
            _customerRepository = customerService;
            _workContext = workContext;
            _env = env;
        }
        #endregion
        #region Methods
        //Get Members
        public List<Customer> GetMembers(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var customers = _customerRepository.TableNoTracking.Include(m=>m.CustomerRoles).Where(c=>c.Deleted==false && c.CustomerRoles.Any(m=>m.Name=="Registered"));

            if (!string.IsNullOrEmpty(searchValue))
            {
                customers = customers.Where(c => c.Username.ToLower().Contains(searchValue.ToLower()) || c.Email.ToLower().Contains(searchValue.ToLower()) || c.Mobile.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
               
                    customers = customers.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            customers = customers.OrderByDescending(c => c.Username).Skip(start).Take(length);

            return customers.ToList();
        }

        // Get Customer Details
        public Customer GetMemberDetails(int id)
        {
            var customer = _customerRepository.TableNoTracking.Include(c=>c.CustomerRoles).Where(c => c.Id == id&&c.Deleted==false && c.CustomerRoles.Any(m=>m.Name=="Registered")).FirstOrDefault();

            if (customer != null)
                return customer;
            return null;
        }

        //Get Online Members
       public List<Customer> GetOnlineMembers(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
           var members=_customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Where(c => c.Active && c.Deleted==false && c.CustomerRoles.Any(m=>m.Name=="Registered"));

            if (!string.IsNullOrEmpty(searchValue))
            {
                members = members.Where(c => c.Username.ToLower().Contains(searchValue.ToLower()) || c.Email.ToLower().Contains(searchValue.ToLower()) || c.Mobile.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                if (sortDirection == "des")
                    members = members.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            members = members.OrderByDescending(c => c.Username).Skip(start).Take(length);

            return members.ToList();

        }

        //Get Consultants
        public List<Customer> GetConsultants(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var Consultants = _customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Include(c => c.CustomerRoles).Where(c => c.Deleted == false && c.CustomerRoles.Any(x=>x.Name=="Consultant"));

            if (!string.IsNullOrEmpty(searchValue))
            {
                Consultants = Consultants.Where(c => c.Username.ToLower().Contains(searchValue.ToLower()) || c.Email.ToLower().Contains(searchValue.ToLower()) || c.Mobile.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
               
                    Consultants = Consultants.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            Consultants = Consultants.OrderByDescending(c => c.Username).Skip(start).Take(length);

            return Consultants.ToList();
        }

        //Get Consultants for Notifications
        public List<Customer> GetConsultants()
        {
            var Consultants = _customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Include(c => c.CustomerRoles).Where(c => c.Deleted == false && c.CustomerRoles.Any(x => x.Name == "Consultant"));

           


            return Consultants.ToList();
        }


        //Get Online Consultants
        public List<Customer> GetOnlineConsultants(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var consultants = _customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Where(c => c.Active && c.Deleted==false&&c.CustomerRoles.Any(x=>x.Name=="Consultant"));

            if (!string.IsNullOrEmpty(searchValue))
            {
                consultants = consultants.Where(c => c.Username.ToLower().Contains(searchValue.ToLower()) || c.Email.ToLower().Contains(searchValue.ToLower()) || c.Mobile.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                if (sortDirection == "des")
                    consultants = consultants.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            consultants = consultants.OrderByDescending(c => c.Username).Skip(start).Take(length);

            return consultants.ToList();
        }

        // Get Consultant Details
        public Customer GetConsultantDetails(int id)
        {
            var consultant = _customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Where(c => c.Id == id&&c.Deleted==false&&c.CustomerRoles.Any(x=>x.Name=="Consultant")).FirstOrDefault();

            if (consultant != null)
                return consultant;
            return null;
        }


        public int GetMembersNumber()
        {
            var query = _customerRepository.TableNoTracking.Include(c=>c.CustomerRoles).Where(c=>c.CustomerRoles.Any(r=>r.Name=="Registered")).Count();

            return query;
        }
        //Get  Consultants number
        public int GetConsultantsNumber()
        {
            var query = _customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Where(c => c.CustomerRoles.Any(r => r.Name == "Consultant")).Count();

            return query;
        }
        //Get  Online Members number
        public int GetOnlineMembersNumber()
        {
            var query = _customerRepository.TableNoTracking.Include(c=>c.CustomerRoles).Where(c => c.Active == true && c.CustomerRoles.Any(n=>n.Name=="Registered")).Count();

            return query;
        }
        //Get  Online Consultants number
        public int GetOnlineConsultantsNumber()
        {
            var query = _customerRepository.TableNoTracking.Include(c => c.CustomerRoles).Where(c => c.Active == true && c.CustomerRoles.Any(n => n.Name == "Registered")).Count();

            return query;
        }

        //Delete Customer
        public void DeleteMember(int id)
        {

           var customer= _customerRepository.Table.Where(c => c.Id == id).FirstOrDefault();

            if(customer !=null)
            {
                customer.Deleted = true;
            }
            _customerRepository.Update(customer);
        }
        #endregion
    }
}
