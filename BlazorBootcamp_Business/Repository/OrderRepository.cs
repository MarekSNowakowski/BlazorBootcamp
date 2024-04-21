using AutoMapper;
using BlazorBootcamp_Business.Repository.IRepository;
using BlazorBootcamp_Common;
using BlazorBootcamp_DataAccess;
using BlazorBootcamp_DataAccess.Data;
using BlazorBootcamp_DataAccess.ViewModel;
using BlazorBootcamp_Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBootcamp_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
            try
            {
                Order obj = _mapper.Map<OrderDTO, Order>(orderDTO);
                _db.OrderHeaders.Add(obj.Header);
                await _db.SaveChangesAsync();

                foreach(var details in  obj.Details)
                {
                    details.OrderHeaderId = obj.Header.Id;
                    _db.OrderDetails.Add(details);
                }

                _db.OrderDetails.AddRange(obj.Details);
                await _db.SaveChangesAsync();

                return new OrderDTO()
                {
                    Header = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.Header),
                    Details = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.Details).ToList()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id); 
            if(objHeader != null)
            {
                IEnumerable<OrderDetail> objDetails = _db.OrderDetails.Where(u=> u.OrderHeaderId == id);

                _db.OrderDetails.RemoveRange(objDetails);
                _db.OrderHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }

            return 0;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _db.OrderDetails;

            foreach(OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    Header = header,
                    Details = orderDetailList.Where(u => u.OrderHeaderId == header.Id)
                };

                OrderFromDb.Add(order);
            }
            // todo: do some filtering

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);
        }

        public async Task<OrderDTO> GetById(int id)
        {
            Order order = new()
            {
                Header = _db.OrderHeaders.FirstOrDefault(u => u.Id == id),
                Details = _db.OrderDetails.Where(u => u.OrderHeaderId == id)
            };
            if(order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
            return new OrderDTO();
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccesful(int id)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if(data == null)
            {
                return new OrderHeaderDTO();
            }
            if(data.Status == SD.Status_Pending)
            {
                data.Status = SD.Status_Confirmed;
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }

            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO orderHeaderDTO)
        {
            if(orderHeaderDTO != null)
            {
                var OrderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(orderHeaderDTO);
                _db.OrderHeaders.Update(OrderHeader);
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(OrderHeader);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
                
            data.Status = status;
            
            if(status == SD.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
