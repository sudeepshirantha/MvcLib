using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.ImageLib
{
    public class ImageUtil
    {
        private static ImageUtil instance = null;
        public Dictionary<string, string> IMAGES = new Dictionary<string, string>();
        public bool _hasInit = false;

        public ImageUtil()
        {
        }

        public static ImageUtil GetDefault()
        {
            if(instance == null){
                instance = new ImageUtil();
            }
            return instance;
        }

        public void Add(string key, string value)
        {
            IMAGES.Add(key, value);
        }

        public static void ValidateInit()
        {
            if (!GetDefault()._hasInit)
            {
                throw new Exception("Pdf library images are not initialized yet!");
            };
        }
    }
}
