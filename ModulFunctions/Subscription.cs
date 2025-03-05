using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DllUnit
{
    public class Subscription
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }


        public List<Subscription> _subscriptions = new List<Subscription>();

        public Subscription Create(Subscription subscription)
        {
            subscription.Id = _subscriptions.Count + 1;
            _subscriptions.Add(subscription);
            return subscription;
        }

        public void Delete(int id)
        {
            var subscription = _subscriptions.FirstOrDefault(x => x.Id == id);
            if (subscription != null)
                _subscriptions.Remove(subscription);
        }

        public Subscription Update(Subscription subscription)
        {
            var existing = _subscriptions.FirstOrDefault(x => x.Id == subscription.Id);
            if (existing != null)
            {
                existing.UserId = subscription.UserId;
                existing.Type = subscription.Type;
                existing.Status = subscription.Status;
                existing.StartDate = subscription.StartDate;
            }
            return existing;
        }

        public Subscription GetById(int id)
        {
            return _subscriptions.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Subscription> GetAll()
        {
            return _subscriptions;
        }

        public IEnumerable<Subscription> GetByUser(string userId)
        {
            return _subscriptions.Where(x => x.UserId == userId);
        }

        public IEnumerable<Subscription> GetByStatus(string status)
        {
            return _subscriptions.Where(x => x.Status == status);
        }

        public int GetCount()
        {
            return _subscriptions.Count;
        }

        public IEnumerable<Subscription> GetByType(string type)
        {
            return _subscriptions.Where(x => x.Type == type);
        }

        public IEnumerable<Subscription> GetByStartDate(DateTime date)
        {
            return _subscriptions.Where(x => x.StartDate.Date == date.Date);
        }
    }
}
