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
            return _context.Settings
                .AsNoTracking()
                .ToDictionary(m => m.Key, m => m.Value);
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.AsNoTracking().ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task EditAsync(SettingEditVM setting)
        {
            var dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);
            if (dbSetting == null) throw new Exception("Setting not found");

            if (setting.Photo != null)
            {
                // Köhnə şəkli sil
                string oldPath = _env.GetFilePath("assets/img/settings", dbSetting.Value);

                string extension = Path.GetExtension(setting.Photo.FileName);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(setting.Photo.FileName);
                string fileName = $"{nameWithoutExt}-{Guid.NewGuid()}{extension}";

                string newPath = _env.GetFilePath("assets/img/settings", fileName);

                dbSetting.Value = fileName;

                _context.Settings.Update(dbSetting);
                await _context.SaveChangesAsync();

                if (File.Exists(oldPath))
                    File.Delete(oldPath);

                await setting.Photo.SaveFileAsync(newPath);
            }
            else
            {
                _mapper.Map(setting, dbSetting);
                _context.Settings.Update(dbSetting);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(SettingCreateVM setting)
        {
            var newSetting = new Setting
            {
                Key = setting.Key
            };

            if (setting.Photo != null)
            {
                string extension = Path.GetExtension(setting.Photo.FileName);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(setting.Photo.FileName);
                string fileName = $"{nameWithoutExt}-{Guid.NewGuid()}{extension}";

                string path = _env.GetFilePath("assets/img/settings", fileName);

                newSetting.Value = fileName;
                await setting.Photo.SaveFileAsync(path);
            }
            else
            {
                newSetting.Value = setting.Value;
            }

            await _context.Settings.AddAsync(newSetting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null) return;

            if (!string.IsNullOrEmpty(setting.Value) &&
                (setting.Value.EndsWith(".png") || setting.Value.EndsWith(".jpg") ||
                 setting.Value.EndsWith(".jpeg") || setting.Value.EndsWith(".gif")))
            {
                string filePath = _env.GetFilePath("assets/img/settings", setting.Value);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
        }
    }
}
