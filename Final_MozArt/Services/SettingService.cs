using AutoMapper;
using Final_MozArt.Data;
using Final_MozArt.Helpers.Extensions;
using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Setting;
using Microsoft.EntityFrameworkCore;

namespace Final_MozArt.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SettingService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task EditAsync(SettingEditVM setting)
        {
            var dbSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == setting.Id);
            if (dbSetting == null) throw new Exception("Setting not found");

            if (setting.Photo != null)
            {
                if (!setting.Photo.CheckFileType("image/"))
                    throw new Exception("Only image files allowed");

                if (!setting.Photo.CheckFileSize(2048))
                    throw new Exception("Max file size is 2MB");

                string extension = Path.GetExtension(setting.Photo.FileName);
                string fileName = $"{Guid.NewGuid()}{extension}";
                string newPath = _env.GetFilePath("assets/img/settings", fileName);
                string oldPath = _env.GetFilePath("assets/img/settings", dbSetting.Value);

                await setting.Photo.SaveFileAsync(newPath);

                if (File.Exists(oldPath)) File.Delete(oldPath);

                dbSetting.Value = fileName;
            }
            else
            {
                _mapper.Map(setting, dbSetting);
            }

            _context.Settings.Update(dbSetting);
            await _context.SaveChangesAsync();
        }
    }
}

