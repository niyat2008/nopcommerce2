using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Notification
{
  public class NotificationService:INotificationService
    {
        #region Fields
        private readonly IRepository<Z_Consultant_Notification> _notidficationRepository;
        #endregion


        #region  Ctor
        public NotificationService(IRepository<Z_Consultant_Notification> notidficationRepository)
        {
            this._notidficationRepository = notidficationRepository;
        }
        #endregion


        #region   Methods

        //Add Post Notification
        public bool AddPostNotification(NotificationModel model,List<int> consultantId)
        {
            var DbModel = new Z_Consultant_Notification
            {
                PostId=model.PostId,
               
                UserId=model.UserId,
                Type=model.Type,
                Time=DateTime.Now,
                IsRead=false
            };

            if(DbModel != null)
            {
                foreach (var con in consultantId)
                {
                    DbModel.OwnerId = con;
                    _notidficationRepository.Insert(DbModel);
                }
                return true;
            }
            return false;
        }

        //Add Comment Notification
        public bool AddCommentNotification(NotificationModel model)
        {
            var DbModel = new Z_Consultant_Notification
            {
                PostId = model.PostId,
                OwnerId=model.OwnerId,
                UserId = model.UserId,
                Type = model.Type,
                Time = DateTime.Now,
                IsRead = false
            };

            if (DbModel != null)
            {
              
                    _notidficationRepository.Insert(DbModel);
               
                return true;
            }

            return false;
        }

        //Get Notifications
        public List<Z_Consultant_Notification> GetNotifications(int userId)
        {
            var query = _notidficationRepository.TableNoTracking.Include(p => p.Post).Include(p => p.User).Include(p => p.Owner).Where(n=>n.OwnerId==userId );

            query = query.OrderByDescending(n => n.Time);

            return query.ToList();
        }

        //Get Un Read Notifications
        public int GetUnReadNotifications(int userId)
        {
            var query = _notidficationRepository.TableNoTracking.Include(p => p.Post).Include(p => p.User).Include(p => p.Owner).Where(n => n.OwnerId == userId && n.IsRead==false);

            int count = query.Count();

            return count;
        }

        //Update Notification
        public void UpdateNotification(int customerId)
        {
           
                var query = _notidficationRepository.Table.Where(n => n.OwnerId == customerId);
            foreach(var q in query)
            {
                q.IsRead = true;
            }
                _notidficationRepository.Update(query);

           

        }

        public void DeleteNotifications(int id)
        {
            var userNotifications = _notidficationRepository.Table.Where(m => m.OwnerId == id).ToList();

            foreach (var item in userNotifications)
            {
                _notidficationRepository.Delete(item);
            } 
        }



        #endregion
    }
}
