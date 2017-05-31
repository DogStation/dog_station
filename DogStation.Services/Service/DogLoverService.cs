using DogStation.Repository;
using DogStation.Entity.Models;
using DogStation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.Net.Http;

namespace DogStation.Services
{
    public class DogLoverService
    {
        [Dependency]
        public DonateRepository donateDao { get; set; }
        [Dependency]
        public DogLoverRepository loverDao { get; set; }
        [Dependency]
        public InventoryRepository inventoryDao { get; set; }
        [Dependency]
        public CommentRepository commentDao { get; set; }
        [Dependency]
        public QiniuService qiniuService { get; set; }

        public List<Dictionary<string, object>> SeeDonations(long userId)
        {
            if (userId == 0) return null;
            return ConverterUtil.ConvertDonations(donateDao.GetDonations(userId));
        }

        public bool Update(DogLover lover)
        {
            if (loverDao.GetId(lover.name) == 0)
                return false;
            return loverDao.Update(lover);
        }

        public DogLover ProcessUpload(MultipartFormDataStreamProvider provider)
        {
            string filename = provider.FileData[0].Headers.ContentDisposition.FileName;
            filename = HttpUtility.HtmlDecode(filename.Trim(new char[] { '\\', '"' }));
            string filepath = provider.FileData[0].LocalFileName;
            string figure = qiniuService.UploadFile(filepath, filename);
            DogLover lover = new DogLover()
            {
                name = HttpUtility.HtmlDecode(provider.FormData.Get(0)),
                gender = provider.FormData.Get(1),
                tel = provider.FormData.Get(2),
                email = provider.FormData.Get(3)
            };
            lover.figure = figure;
            return lover;
        }

        public MyStatusCode CommentDog(Comment comment)
        {
            MyStatusCode state = MyStatusCode.Validated;
            if (comment.commenter == 0)
                state = MyStatusCode.Invalid;
            else
            {
                try
                {
                    commentDao.Add(comment);
                }
                catch (Exception)
                {
                    state = MyStatusCode.InternalServerError;
                }
            }
            return state;
        }

        public MyStatusCode Donate(List<DonateItem> items, long userId)
        {
            MyStatusCode state = MyStatusCode.Validated;
            if (userId == 0)
                state = MyStatusCode.Invalid;
            else
            {
                int lovesInc = CalLovesInc(items);
                DonateRecord record = new DonateRecord()
                {
                    lover = userId,
                    items = items.Count(),
                    lovesInc = lovesInc,
                    donateTime = DateTime.Now
                };

                using (var dbTransaction = loverDao.db.Database.BeginTransaction())
                {
                    try
                    {
                        long recordId = donateDao.Add(record).idDonateRecord;

                        foreach (DonateItem item in items)
                        {
                            item.record = recordId;
                        }
                        donateDao.Donate(items);

                        inventoryDao.Update(items);

                        loverDao.IncLoves(userId, lovesInc);
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        state = MyStatusCode.InternalServerError;
                    }
                }
            }
            return state;
        }

        public List<Dictionary<string, object>> SeeComments(long idDog, int page)
        {
            return ConverterUtil.ConvertComments(commentDao.GetAll(idDog, page));
        }

        private int CalLovesInc(List<DonateItem> items)
        {
            return 10 * items.Count();
        }
    }
}