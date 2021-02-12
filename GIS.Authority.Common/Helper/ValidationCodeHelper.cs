using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.DrawingCore.Text;
using System.IO;
using System.Text;

namespace GIS.Authority.Common
{
    #region 验证码生成类

    /// <summary>
    /// 边框样式
    /// </summary>
    public enum BorderStyle
    {
        /// <summary>
        /// 无边框
        /// </summary>
        None,

        /// <summary>
        /// 矩形边框
        /// </summary>
        Rectangle,

        /// <summary>
        /// 圆角边框
        /// </summary>
        RoundRectangle
    }

    /// <summary>
    /// 验证码生成类
    /// </summary>
    public class ValidationCodeHelper
    {
        #region 定义和初始化配置字段

        /// <summary>
        /// 生成的验证码字符串
        /// </summary>
        public char[] Chars;

        /// <summary>
        /// 用户存取验证码字符串
        /// </summary>
        private string _validationCode = string.Empty;

        /// <summary>
        /// 验证码长度
        /// </summary>
        private readonly int _validationCodeCount = 4;

        /// <summary>
        /// 验证码的宽度，默认为80
        /// </summary>
        private readonly int _bgWidth = 130;

        /// <summary>
        /// 验证码的高度，默认为40
        /// </summary>
        private readonly int _bgHeight = 40;

        /// <summary>
        /// 验证码字符串随机转动的角度的最大值
        /// </summary>
        private readonly int _rotationAngle = 40;

        ///// <summary>
        ///// 验证码字体列表，默认为{ "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" }
        ///// </summary>
        //private string[] fontFace = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

        /// <summary>
        /// 验证码字体的最小值，默认为15,建议不小于15像素
        /// </summary>
        private readonly int _fontMinSize = 20;

        /// <summary>
        /// 验证码字体的最大值，默认为20
        /// </summary>
        private readonly int _fontMaxSize = 25;

        /// <summary>
        /// 验证码的背景色，默认为Color.FromArgb(243, 251, 254)
        /// </summary>
        private readonly Color _backColor = Color.FromArgb(238, 238, 238);

        /// <summary>
        /// 直线条数，默认为3条
        /// </summary>
        private readonly int _lineCount = 0;

        private Random _random = new Random();

        /// <summary>
        /// 是否添加噪点，默认添加，噪点颜色为系统随机生成
        /// </summary>
        private readonly bool _isPixel = false;

        /// <summary>
        /// 随机背景字符串的个数
        /// </summary>
        private readonly int _randomStringCount;

        /// <summary>
        /// 随机背景字符串的大小
        /// </summary>
        private readonly int _randomStringFontSize = 9;

        /// <summary>
        /// 设置或获取边框样式
        /// </summary>
        private readonly BorderStyle _border;

        private Point[] _strPoint;

        /// <summary>
        /// 对验证码图片进行高斯模糊的阀值，如果设置为0，则不对图片进行高斯模糊，该设置可能会对图片处理的性能有较大影响
        /// </summary>
        private double _gaussianDeviation;

        ///// <summary>
        ///// 对图片进行暗度和亮度的调整，如果该值为0，则不调整。该设置会对图片处理性能有较大影响
        ///// </summary>
        //private int _brightnessValue;
        private Graphics _dc;

        public static ValidationCodeHelper mValidationCodeHelper;

        public static ValidationCodeHelper GetInstance()
        {
            if (mValidationCodeHelper == null)
            {
                mValidationCodeHelper = new ValidationCodeHelper();
            }
            return mValidationCodeHelper;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ValidationCodeHelper()
        {
            _randomStringCount = 0;
            _border = BorderStyle.None;
        }

        #endregion 定义和初始化配置字段

        /// <summary>
        /// 构造函数，用于初始化常用变量
        /// </summary>
        public void DrawValidationCode()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
            _strPoint = new Point[_validationCodeCount + 1];
            if (_gaussianDeviation < 0)
            {
                _gaussianDeviation = 0;
            }
        }

        #region 生成验证码图片

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">用于存储图片的一般字节序列</param>
        /// <returns>返回图片</returns>>
        public byte[] CreateImage(string code)
        {
            var target = new MemoryStream();
            var bit = new Bitmap(_bgWidth + 1, _bgHeight + 1);

            //写字符串
            _dc = Graphics.FromImage(bit);
            _dc.SmoothingMode = SmoothingMode.HighQuality;
            _dc.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _dc.InterpolationMode = InterpolationMode.HighQualityBilinear;
            _dc.CompositingQuality = CompositingQuality.HighQuality;

            try
            {
                _dc.Clear(Color.White);
                DrawValidationCode();
                _dc.DrawImageUnscaled(DrawBackground(), 0, 0);
                _dc.DrawImageUnscaled(DrawRandomString(code), 0, 0);

                //对图片文字进行扭曲
                bit = TwistImage(bit, true, 3, 4);

                //对图片进行高斯模糊
                if (_gaussianDeviation > 0)
                {
                    var gau = new Gaussian();
                    bit = gau.FilterProcessImage(_gaussianDeviation, bit);
                }

                ////进行暗度和亮度处理
                //if (_brightnessValue != 0)
                //{
                //    //对图片进行调暗处理
                //    bit = AdjustBrightness(bit, _brightnessValue);
                //}
                bit.Save(target, ImageFormat.Jpeg);

                //输出图片流
                return target.ToArray();
            }
            finally
            {
                //brush.Dispose();
                bit.Dispose();
                _dc.Dispose();
            }
        }

        public byte[] CreateVaildCodeImage(int codeType, int length)
        {
            string code = CreateCode(codeType, length);
            return CreateImage(code);
        }

        #endregion 生成验证码图片

        #region 生成验证码

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="codeType">1-纯数字,2-数字+字母,3-中文</param>
        /// <param name="length">长度</param>
        /// <returns>验证码</returns>
        public string CreateCode(int codeType, int length)
        {
            string code;
            switch (codeType)
            {
                case 1:
                    code = CreateNumCode(length);
                    break;

                case 2:
                    code = CreateMixCode(length);
                    break;

                case 3:
                    code = CreateChiCode(length);
                    break;

                default:
                    code = CreateNumCode(length);
                    break;
            }

            return code;
        }

        /// <summary>
        /// 产生数字+字母验证码
        /// 算法思想：将所有数字及字母存储在字符串中，调用Random()函数随机选取子字符串数组中的一个字符，加入
        /// 指定的字符串末尾，如此反复，最终返回生成好的验证码
        /// </summary>
        /// <param name="codeLength">验证码长度</param>
        /// <returns>验证码</returns>
        private static string CreateMixCode(int codeLength)
        {
            var so = "2,3,4,5,6,7,8,9,a,s,d,f,g,h,z,c,v,b,n,m,k,q,w,e,r,t,y,u,p,A,S,D,F,G,H,Z,C,V,B,N,M,K,Q,W,E,R,T,Y,U,P";
            var strArr = so.Split(',');
            var code = string.Empty;
            var rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code += strArr[rand.Next(0, strArr.Length)];
            }

            return code;
        }

        /// <summary>
        /// 生成纯数字验证码
        /// </summary>
        /// <param name="codeCount">验证码个数</param>
        /// <returns>验证码</returns>
        private static string CreateNumCode(int codeCount)
        {
            var allChar = "0,1,2,3,4,5,6,7,8,9";
            var allCharArray = allChar.Split(','); //将allChar的每个由逗号分隔的元素添加到数组中
            var randomCode = string.Empty;
            var temp = -1;
            var rand = new Random();
            for (var i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks)); //更换随机数生成器种子避免产生相同随机数
                }

                //返回一个小于9的非负随机数
                var t = rand.Next(9);

                //保证连续两个生成的随机数不同
                if (temp == t)
                {
                    return CreateNumCode(codeCount);
                }

                temp = t;
                randomCode += allCharArray[t];
            }

            //Session["CheckCode"] = randomCode;
            return randomCode;
        }

        /// <summary>
        /// 此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将
        ///  四个字节数组存储在object数组中。
        /// </summary>
        /// <param name="strLength">字符串长度</param>
        /// <returns>返回中文</returns>
        private static object[] GenChiCode(int strLength)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string[] baseStr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            var rnd = new Random();

            //定义一个object数组用来存放随机生成的汉字
            var bytes = new object[strLength];

            /*
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入object数组中
            每个汉字有四个区位码组
            区位码第1位和区位码第2位作为字节数组第一个元素
            区位码第3位和区位码第4位作为字节数组第二个元素
            */
            for (var i = 0; i < strLength; i++)
            {
                //区位码第1位
                var r1 = rnd.Next(11, 14); //返回一个介于11到14的随机数
                var strR1 = baseStr[r1].Trim();

                //区位码第2位
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i); //更换随机数发生器的种子避免产生重复值
                var r2 = rnd.Next(0, r1 == 13 ? 7 : 16);
                var strR2 = baseStr[r2].Trim();

                //区位码第3位
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                var r3 = rnd.Next(10, 16);
                var strR3 = baseStr[r3].Trim();

                //区位码第4位
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                switch (r3)
                {
                    case 10:
                        r4 = rnd.Next(1, 16);
                        break;

                    case 15:
                        r4 = rnd.Next(0, 15);
                        break;

                    default:
                        r4 = rnd.Next(0, 16);
                        break;
                }

                var strR4 = baseStr[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码
                var byte1 = Convert.ToByte(strR1 + strR2, 16);
                var byte2 = Convert.ToByte(strR3 + strR4, 16);

                //将两个字节变量存储在字节数组中
                byte[] strR = { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(strR, i);
            }

            return bytes;
        }

        /// <summary>
        /// 生成指定位数的汉字验证码，并转换为String类型
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>中文字符</returns>
        private static string CreateChiCode(int length)
        {
            var obj = GenChiCode(length);
            var gb = Encoding.GetEncoding("gb2312");
            var bytes = new string[length];
            var code = string.Empty;
            for (var i = 0; i < length; i++)
            {
                bytes[i] = gb.GetString((byte[])Convert.ChangeType(obj[i], typeof(byte[])));
                code = code + bytes[i];
            }

            return code;
        }

        #endregion 生成验证码

        #region 图片去色（图片黑白化）

        /// <summary>
        /// 图片去色（图片黑白化）
        /// </summary>
        /// <param name="original">一个需要处理的图片</param>
        /// <returns>图片</returns>
        private static Bitmap MakeGrayScale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            var newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            var g = Graphics.FromImage(newBitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;

            //create the gray scale ColorMatrix
            var colorMatrix = new ColorMatrix(new[]
            {
                new[] {.3f, .3f, .3f, 0, 0},
                new[] {.59f, .59f, .59f, 0, 0},
                new[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            //create some image attributes
            var attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the gray scale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        #endregion 图片去色（图片黑白化）

        #region 图片任意角度旋转

        /// <summary>
        /// 图片任意角度旋转
        /// </summary>
        /// <param name="bmp">原始图Bitmap</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="bkColor">背景色</param>
        /// <returns>输出Bitmap</returns>
        private static Bitmap KiRotate(Bitmap bmp, float angle, Color bkColor)
        {
            var w = bmp.Width;
            var h = bmp.Height;

            var pf = bkColor == Color.Transparent ? PixelFormat.Format32bppArgb : bmp.PixelFormat;

            var tmp = new Bitmap(w, h, pf);
            var g = Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            var path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            var matrix = new Matrix();
            matrix.Rotate(angle);
            var rct = path.GetBounds(matrix);

            var dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();
            tmp.Dispose();

            return dst;
        }

        #endregion 图片任意角度旋转

        #region 缩放图片

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <param name="mode">缩放质量</param>
        /// <returns>处理以后的图片</returns>
        private static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH, InterpolationMode mode)
        {
            try
            {
                var b = new Bitmap(newW, newH);
                var g = Graphics.FromImage(b);

                // 插值算法的质量
                g.InterpolationMode = mode;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        #endregion 缩放图片

        #region 正弦曲线Wave扭曲图片

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns>图片</returns>
        private static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            const double pi2 = 6.283185307179586476925286766559;

            // 将位图背景填充为白色
            var graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            var baseAxisLen = bXDir ? destBmp.Height : destBmp.Width;

            for (var i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    var dx = bXDir ? pi2 * j / baseAxisLen : pi2 * i / baseAxisLen;
                    dx += dPhase;
                    var dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    var oldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    var oldY = bXDir ? j : j + (int)(dy * dMultValue);

                    var color = srcBmp.GetPixel(i, j);
                    if (oldX >= 0 && oldX < destBmp.Width
                                  && oldY >= 0 && oldY < destBmp.Height)
                    {
                        destBmp.SetPixel(oldX, oldY, color);
                    }
                }
            }

            return destBmp;
        }

        #endregion 正弦曲线Wave扭曲图片

        #region 绘制圆角矩形

        /// <summary>
        /// C# GDI+ 绘制圆角矩形
        /// </summary>
        /// <param name="g">Graphics 对象</param>
        /// <param name="rectangle">Rectangle 对象，圆角矩形区域</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="borderWidth">边框宽度</param>
        /// <param name="r">圆角半径</param>
        private static void DrawRoundRectangle(Graphics g, Rectangle rectangle, Color borderColor, float borderWidth, int r)
        {
            // 如要使边缘平滑，请取消下行的注释
            g.SmoothingMode = SmoothingMode.HighQuality;

            // 由于边框也需要一定宽度，需要对矩形进行修正
            //rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            Pen p = new Pen(borderColor, borderWidth);

            // 调用 getRoundRectangle 得到圆角矩形的路径，然后再进行绘制
            g.DrawPath(p, GetRoundRectangle(rectangle, r));
        }

        #endregion 绘制圆角矩形

        #region 根据普通矩形得到圆角矩形的路径

        /// <summary>
        /// 根据普通矩形得到圆角矩形的路径
        /// </summary>
        /// <param name="rectangle">原始矩形</param>
        /// <param name="r">半径</param>
        /// <returns>图形路径</returns>
        private static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            var l = 2 * r;

            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中
            var gp = new GraphicsPath();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);

            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);

            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            return gp;
        }

        #endregion 根据普通矩形得到圆角矩形的路径

        #region 增加或減少亮度

        /// <summary>
        /// 增加或減少亮度
        /// </summary>
        /// <param name="img">System.Drawing.Image Source </param>
        /// <param name="valBrightness">0~255</param>
        /// <returns>图片</returns>
        private static Bitmap AdjustBrightness(Image img, int valBrightness)
        {
            // 讀入欲轉換的圖片並轉成為 Bitmap
            var bitmap = new Bitmap(img);

            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    // 取得每一個 pixel
                    var pixel = bitmap.GetPixel(x, y);

                    // 判斷 如果處理過後 255 就設定為 255 如果小於則設定為 0
                    var r = ((pixel.R + valBrightness > 255) ? 255 : pixel.R + valBrightness) < 0 ? 0 : ((pixel.R + valBrightness > 255) ? 255 : pixel.R + valBrightness);
                    var g = ((pixel.G + valBrightness > 255) ? 255 : pixel.G + valBrightness) < 0 ? 0 : ((pixel.G + valBrightness > 255) ? 255 : pixel.G + valBrightness);
                    var b = ((pixel.B + valBrightness > 255) ? 255 : pixel.B + valBrightness) < 0 ? 0 : ((pixel.B + valBrightness > 255) ? 255 : pixel.B + valBrightness);

                    // 將改過的 RGB 寫回
                    var newColor = Color.FromArgb(pixel.A, r, g, b);

                    bitmap.SetPixel(x, y, newColor);
                }
            }

            // 回傳結果
            return bitmap;
        }

        #endregion 增加或減少亮度

        #region 画验证码背景，例如，增加早点，添加曲线和直线等

        /// <summary>
        /// 画验证码背景，例如，增加早点，添加曲线和直线等
        /// </summary>
        /// <returns>背景图片</returns>
        private Bitmap DrawBackground()
        {
            var bit = new Bitmap(_bgWidth + 1, _bgHeight + 1);
            var g = Graphics.FromImage(bit);
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(Color.White);
            var rectangle = new Rectangle(0, 0, _bgWidth, _bgHeight);
            var brush = new SolidBrush(_backColor);
            g.FillRectangle(brush, rectangle);

            //画噪点
            if (_isPixel)
            {
                g.DrawImageUnscaled(DrawRandomPixel(30), 0, 0);
            }

            g.DrawImageUnscaled(DrawRandBgString(), 0, 0);

            //画曲线
            //g.DrawImageUnscaled(DrawRandomBezier(bezierCount), 0, 0);
            //画直线
            g.DrawImageUnscaled(DrawRandomLine(_lineCount), 0, 0);

            if (_border == BorderStyle.Rectangle)
            {
                //绘制边框
                g.DrawRectangle(new Pen(Color.FromArgb(90, 87, 46)), 0, 0, _bgWidth, _bgHeight);
            }
            else if (_border == BorderStyle.RoundRectangle)
            {
                //画圆角
                DrawRoundRectangle(g, rectangle, Color.FromArgb(90, 87, 46), 1, 3);
            }

            return bit;
        }

        #endregion 画验证码背景，例如，增加早点，添加曲线和直线等

        #region 随机生成贝塞尔曲线

        /// <summary>
        /// 随机生成贝塞尔曲线
        /// </summary>
        /// <param name="lineNum">线条数量</param>
        /// <returns>图片</returns>
        private Bitmap DrawRandomBezier(int lineNum)
        {
            var b = new Bitmap(_bgWidth, _bgHeight);
            b.MakeTransparent();
            var g = Graphics.FromImage(b);
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var graphicsPath = new GraphicsPath();
            var lineRandNum = _random.Next(lineNum);

            for (var i = 0; i < lineNum - lineRandNum; i++)
            {
                var p = new Pen(GetRandomDeepColor());
                Point[] point =
                {
                    new Point(_random.Next(1, b.Width / 10), _random.Next(1, b.Height)),
                    new Point(_random.Next((b.Width / 10) * 2, (b.Width / 10) * 4), _random.Next(1, b.Height)),
                    new Point(_random.Next((b.Width / 10) * 4, (b.Width / 10) * 6), _random.Next(1, b.Height)),
                    new Point(_random.Next((b.Width / 10) * 8, b.Width), _random.Next(1, b.Height))
                };

                graphicsPath.AddBeziers(point);
                g.DrawPath(p, graphicsPath);
                p.Dispose();
            }

            for (var i = 0; i < lineRandNum; i++)
            {
                var p = new Pen(GetRandomDeepColor());
                Point[] point =
                {
                    new Point(_random.Next(1, b.Width), _random.Next(1, b.Height)),
                    new Point(_random.Next((b.Width / 10) * 2, b.Width), _random.Next(1, b.Height)),
                    new Point(_random.Next((b.Width / 10) * 4, b.Width), _random.Next(1, b.Height)),
                    new Point(_random.Next(1, b.Width), _random.Next(1, b.Height))
                };
                graphicsPath.AddBeziers(point);
                g.DrawPath(p, graphicsPath);
                p.Dispose();
            }

            return b;
        }

        #endregion 随机生成贝塞尔曲线

        #region 画直线

        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="lineNum">线条个数</param>
        /// <returns>图片</returns>
        private Bitmap DrawRandomLine(int lineNum)
        {
            if (lineNum < 0)
            {
                throw new ArgumentNullException(nameof(lineNum));
            }

            var b = new Bitmap(_bgWidth, _bgHeight);
            b.MakeTransparent();
            var g = Graphics.FromImage(b);
            g.Clear(Color.Transparent);
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            for (var i = 0; i < lineNum; i++)
            {
                var p = new Pen(GetRandomDeepColor());
                var pt1 = new Point(_random.Next(1, (b.Width / 5) * 2), _random.Next(b.Height));
                var pt2 = new Point(_random.Next((b.Width / 5) * 3, b.Width), _random.Next(b.Height));
                g.DrawLine(p, pt1, pt2);
                p.Dispose();
            }

            return b;
        }

        #endregion 画直线

        #region 画随机噪点

        /// <summary>
        /// 画随机噪点
        /// </summary>
        /// <param name="pixNum">噪点的百分比</param>
        /// <returns>图片</returns>
        private Bitmap DrawRandomPixel(int pixNum)
        {
            var b = new Bitmap(_bgWidth, _bgHeight);
            b.MakeTransparent();
            var graph = Graphics.FromImage(b);
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.InterpolationMode = InterpolationMode.HighQualityBilinear;

            //画噪点
            for (var i = 0; i < (_bgHeight * _bgWidth) / pixNum; i++)
            {
                var x = _random.Next(b.Width);
                var y = _random.Next(b.Height);
                b.SetPixel(x, y, GetRandomDeepColor());

                //下移坐标重新画点
                if ((x + 1) < b.Width && (y + 1) < b.Height)
                {
                    //画图片的前景噪音点
                    graph.DrawRectangle(new Pen(Color.Silver), _random.Next(b.Width), _random.Next(b.Height), 1, 1);
                }
            }

            return b;
        }

        #endregion 画随机噪点

        #region 写入验证码的字符串

        /// <summary>
        /// 写入验证码的字符串
        /// </summary>
        /// <param name="code">字符串</param>
        /// <returns>图片</returns>
        private Bitmap DrawRandomString(string code)
        {
            if (_fontMaxSize >= (_bgHeight / 5) * 4)
            {
                throw new ArgumentException("字体最大值参数FontMaxSize与验证码高度相近，这会导致描绘验证码字符串时出错，请重新设置参数！");
            }

            var b = new Bitmap(_bgWidth, _bgHeight);
            b.MakeTransparent();
            var g = Graphics.FromImage(b);

            g.Clear(Color.Transparent);
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            Chars = code.ToCharArray(); //拆散字符串成单字符数组
            _validationCode = Chars.ToString();

            //设置字体显示格式
            var format = new StringFormat(StringFormatFlags.NoClip)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var f = new FontFamily(GenericFontFamilies.Monospace);
            var charNum = Chars.Length;

            var point1 = new Point();
            for (var i = 0; i < _validationCodeCount; i++)
            {
                //定义字体
                var textFont = new Font(f, _random.Next(_fontMinSize, _fontMaxSize), FontStyle.Bold);

                //定义画刷，用于写字符串
                //Brush brush = new SolidBrush(GetRandomDeepColor());
                var textFontSize = Convert.ToInt32(textFont.Size);
                var point = new Point(_random.Next((_bgWidth / charNum) * i + 5, (_bgWidth / charNum) * (i + 1)), _random.Next(_bgHeight / 5 + textFontSize / 2, _bgHeight - textFontSize / 2));

                //如果当前字符X坐标小于字体的二分之一大小
                if (point.X < textFontSize / 2)
                {
                    point.X = point.X + textFontSize / 2;
                }

                //防止文字叠加
                if (i > 0 && (point.X - point1.X < (textFontSize / 2 + textFontSize / 2)))
                {
                    point.X = point.X + textFontSize;
                }

                //如果当前字符X坐标大于图片宽度，就减去字体的宽度
                if (point.X > (_bgWidth - textFontSize / 2))
                {
                    point.X = _bgWidth - textFontSize / 2;
                }

                point1 = point;

                var angle = _random.Next(-_rotationAngle, _rotationAngle); //转动的度数
                g.TranslateTransform(point.X, point.Y); //移动光标到指定位置
                g.RotateTransform(angle);

                //设置渐变画刷
                var rectangle = new Rectangle(0, 1, Convert.ToInt32(textFont.Size), Convert.ToInt32(textFont.Size));
                var c = GetRandomDeepColor();
                var brush = new LinearGradientBrush(rectangle, c, GetLightColor(c, 120), _random.Next(180));

                g.DrawString(Chars[i].ToString(), textFont, brush, 1, 1, format);

                g.RotateTransform(-angle); //转回去
                g.TranslateTransform(-point.X, -point.Y); //移动光标到指定位置，每个字符紧凑显示，避免被软件识别

                _strPoint[i] = point;

                textFont.Dispose();
                brush.Dispose();
            }

            return b;
        }

        #endregion 写入验证码的字符串

        #region 画干扰背景文字

        /// <summary>
        /// 画背景干扰文字
        /// </summary>
        /// <returns>图片</returns>
        private Bitmap DrawRandBgString()
        {
            var b = new Bitmap(_bgWidth, _bgHeight);
            string[] randStr = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            b.MakeTransparent();
            var g = Graphics.FromImage(b);

            g.Clear(Color.Transparent);
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            //设置字体显示格式
            var format = new StringFormat(StringFormatFlags.NoClip)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var f = new FontFamily(GenericFontFamilies.Serif);
            var textFont = new Font(f, _randomStringFontSize, FontStyle.Underline);

            const int randAngle = 60; //随机转动角度

            for (var i = 0; i < _randomStringCount; i++)
            {
                var brush = new SolidBrush(GetRandomLightColor());
                var pot = new Point(_random.Next(5, _bgWidth - 5), _random.Next(5, _bgHeight - 5));

                //随机转动的度数
                float angle = _random.Next(-randAngle, randAngle);

                //转动画布
                g.RotateTransform(angle);
                g.DrawString(randStr[_random.Next(randStr.Length)], textFont, brush, pot, format);

                //转回去，为下一个字符做准备
                g.RotateTransform(-angle);

                //释放资源
                brush.Dispose();
            }

            textFont.Dispose();
            format.Dispose();
            f.Dispose();

            return b;
        }

        #endregion 画干扰背景文字



        #region 根据指定长度，返回随机验证码

        /// <summary>
        /// 根据指定长度，返回随机验证码
        /// </summary>
        /// <param name="length">制定长度</param>
        /// <returns>随即验证码</returns>
        private string Next(int length)
        {
            _validationCode = GetRandomCode(length);
            return _validationCode;
        }

        #endregion 根据指定长度，返回随机验证码

        #region 内部方法：返回指定长度的随机验证码字符串

        /// <summary>
        /// 根据指定大小返回随机验证码
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字符串</returns>
        private string GetRandomCode(int length)
        {
            var sb = new StringBuilder(6);

            for (var i = 0; i < length; i++)
            {
                sb.Append(char.ConvertFromUtf32(RandomAZ09()));
            }

            return sb.ToString();
        }

        #endregion 内部方法：返回指定长度的随机验证码字符串

        #region 内部方法：产生随机数和随机点

        /// <summary>
        /// 产生0-9A-Z的随机字符代码
        /// </summary>
        /// <returns>字符代码</returns>
        private int RandomAZ09()
        {
            var result = 48;
            var ram = new Random();
            var i = ram.Next(2);

            switch (i)
            {
                case 0:
                    result = ram.Next(48, 58);
                    break;

                case 1:
                    result = ram.Next(65, 91);
                    break;
            }

            return result;
        }

        #endregion 内部方法：产生随机数和随机点

        #region 随机生成颜色值

        /// <summary>
        /// 生成随机深颜色
        /// </summary>
        /// <returns>颜色</returns>
        private Color GetRandomDeepColor()
        {
            //int high = 255;
            const int redLow = 160;
            const int greenLow = 100;
            const int blueLow = 160;
            var red = _random.Next(redLow);
            var green = _random.Next(greenLow);
            var blue = _random.Next(blueLow);
            var color = Color.FromArgb(red, green, blue);
            return color;
        }

        /// <summary>
        /// 生成随机浅颜色
        /// </summary>
        /// <returns>randomColor</returns>
        private Color GetRandomLightColor()
        {
            const int low = 180;           //色彩的下限
            const int high = 255;          //色彩的上限
            var red = _random.Next(high) % (high - low) + low;
            var green = _random.Next(high) % (high - low) + low;
            var blue = _random.Next(high) % (high - low) + low;
            var color = Color.FromArgb(red, green, blue);
            return color;
        }

        /// <summary>
        /// 生成随机颜色值
        /// </summary>
        /// <returns>颜色</returns>
        private Color GetRandomColor()
        {
            const int low = 10;           //色彩的下限
            const int high = 255;          //色彩的上限
            var red = _random.Next(high) % (high - low) + low;
            var green = _random.Next(high) % (high - low) + low;
            var blue = _random.Next(high) % (high - low) + low;
            var color = Color.FromArgb(red, green, blue);
            return color;
        }

        /// <summary>
        /// 获取与当前颜色值相加后的颜色
        /// </summary>
        /// <param name="c">颜色</param>
        /// <param name="value">值</param>
        /// <returns>颜色</returns>
        private Color GetLightColor(Color c, int value)
        {
            int red = c.R, green = c.G, blue = c.B;    //越大颜色越浅
            if (red + value < 255 && red + value > 0)
            {
                red = c.R + 40;
            }

            if (green + value < 255 && green + value > 0)
            {
                green = c.G + 40;
            }

            if (blue + value < 255 && blue + value > 0)
            {
                blue = c.B + 40;
            }

            var color = Color.FromArgb(red, green, blue);
            return color;
        }

        #endregion 随机生成颜色值



        #region 滤镜

        /// <summary>
        /// 红色滤镜
        /// </summary>
        /// <param name="bitmap">Bitmap</param>
        /// <param name="threshold">阀值 -255~255</param>
        /// <returns>图片</returns>
        private Bitmap AdjustToRed(Bitmap bitmap, int threshold)
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    // 取得每一個 pixel
                    var pixel = bitmap.GetPixel(x, y);
                    var r = pixel.R + threshold;
                    r = Math.Max(r, 0);
                    r = Math.Min(255, r);

                    // 將改過的 RGB 寫回
                    // 只寫入紅色的值 , G B 都放零
                    var newColor = Color.FromArgb(pixel.A, r, 0, 0);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            // 回傳結果
            return bitmap;
        }

        /// <summary>
        /// 绿色滤镜
        /// </summary>
        /// <param name="bitmap">一个图片实例</param>
        /// <param name="threshold">阀值 -255~+255</param>
        /// <returns>图片</returns>
        private Bitmap AdjustToGreen(Bitmap bitmap, int threshold)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // 取得每一個 pixel
                    var pixel = bitmap.GetPixel(x, y);

                    //判斷是否超過255 如果超過就是255
                    var g = pixel.G + threshold;

                    //如果小於0就為0
                    if (g > 255)
                    {
                        g = 255;
                    }

                    if (g < 0)
                    {
                        g = 0;
                    }

                    // 將改過的 RGB 寫回
                    // 只寫入綠色的值 , R B 都放零
                    var newColor = Color.FromArgb(pixel.A, 0, g, 0);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            // 回傳結果
            return bitmap;
        }

        /// <summary>
        /// 蓝色滤镜
        /// </summary>
        /// <param name="bitmap">一个图片实例</param>
        /// <param name="threshold">阀值 -255~255</param>
        /// <returns>图片</returns>
        private Bitmap AdjustToBlue(Bitmap bitmap, int threshold)
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    // 取得每一個 pixel
                    var pixel = bitmap.GetPixel(x, y);

                    //判斷是否超過255 如果超過就是255
                    var b = pixel.B + threshold;

                    //如果小於0就為0
                    if (b > 255)
                    {
                        b = 255;
                    }

                    if (b < 0)
                    {
                        b = 0;
                    }

                    // 將改過的 RGB 寫回
                    // 只寫入藍色的值 , R G 都放零
                    var newColor = Color.FromArgb(pixel.A, 0, 0, b);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            // 回傳結果
            return bitmap;
        }

        /// <summary>
        /// 调整 RGB 色调
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="thresholdRed">红色阀值</param>
        /// <param name="thresholdGreen">绿色阀值</param>
        /// <param name="thresholdBlue">蓝色阀值</param>
        /// <returns>图片</returns>
        private Bitmap AdjustToCustomColor(Bitmap bitmap, int thresholdRed, int thresholdGreen, int thresholdBlue)
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    // 取得每一個 pixel
                    var pixel = bitmap.GetPixel(x, y);

                    //判斷是否超過255 如果超過就是255
                    var g = pixel.G + thresholdGreen;

                    //如果小於0就為0
                    if (g > 255)
                    {
                        g = 255;
                    }

                    if (g < 0)
                    {
                        g = 0;
                    }

                    //判斷是否超過255 如果超過就是255
                    var r = pixel.R + thresholdRed;

                    //如果小於0就為0
                    if (r > 255)
                    {
                        r = 255;
                    }

                    if (r < 0)
                    {
                        r = 0;
                    }

                    //判斷是否超過255 如果超過就是255
                    var b = pixel.B + thresholdBlue;

                    //如果小於0就為0
                    if (b > 255)
                    {
                        b = 255;
                    }

                    if (b < 0)
                    {
                        b = 0;
                    }

                    // 將改過的 RGB 寫回
                    // 只寫入綠色的值 , R B 都放零
                    var newColor = Color.FromArgb(pixel.A, r, g, b);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return bitmap;
        }

        #endregion 滤镜

        #region 对图片进行雾化效果

        /// <summary>
        /// 对图片进行雾化效果
        /// </summary>
        /// <param name="bmp">图片</param>
        /// <returns>图片</returns>
        private Bitmap Atomization(Bitmap bmp)
        {
            var bmpHeight = bmp.Height;
            var bmpWidth = bmp.Width;
            var newBitmap = new Bitmap(bmpWidth, bmpHeight);
            var oldBitmap = bmp;
            for (var x = 1; x < bmpWidth - 1; x++)
            {
                for (var y = 1; y < bmpHeight - 1; y++)
                {
                    var random = new Random(Guid.NewGuid().GetHashCode());
                    var k = random.Next(123456);

                    //像素块大小
                    int dx = x + k % 19;
                    int dy = y + k % 19;
                    if (dx >= bmpWidth)
                    {
                        dx = bmpWidth - 1;
                    }

                    if (dy >= bmpHeight)
                    {
                        dy = bmpHeight - 1;
                    }

                    var pixel = oldBitmap.GetPixel(dx, dy);
                    newBitmap.SetPixel(x, y, pixel);
                }
            }

            return newBitmap;
        }

        #endregion 对图片进行雾化效果
    }

    #endregion 验证码生成类

    #region 高斯模糊算法

    /// <summary>
    /// 高斯模糊算法
    /// </summary>
    public class Gaussian
    {
        /// <summary>
        /// Calculate1DSampleKernel
        /// </summary>
        /// <param name="deviation">deviation</param>
        /// <param name="size">size</param>
        /// <returns>double</returns>
        public static double[,] Calculate1DSampleKernel(double deviation, int size)
        {
            var ret = new double[size, 1];
            var half = size / 2;
            for (var i = 0; i < size; i++)
            {
                ret[i, 0] = 1 / (Math.Sqrt(2 * Math.PI) * deviation) * Math.Exp(-(i - half) * (i - half) / (2 * deviation * deviation));
            }

            return ret;
        }

        /// <summary>
        /// Calculate1DSampleKernel
        /// </summary>
        /// <param name="deviation">deviation</param>
        /// <returns>double</returns>
        public static double[,] Calculate1DSampleKernel(double deviation)
        {
            var size = (int)Math.Ceiling(deviation * 3) * 2 + 1;
            return Calculate1DSampleKernel(deviation, size);
        }

        /// <summary>
        /// CalculateNormalized1DSampleKernel
        /// </summary>
        /// <param name="deviation">deviation</param>
        /// <returns>double</returns>
        public static double[,] CalculateNormalized1DSampleKernel(double deviation)
        {
            return NormalizeMatrix(Calculate1DSampleKernel(deviation));
        }

        /// <summary>
        /// NormalizeMatrix
        /// </summary>
        /// <param name="matrix">matrix</param>
        /// <returns>double</returns>
        public static double[,] NormalizeMatrix(double[,] matrix)
        {
            var ret = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double sum = 0;
            for (var i = 0; i < ret.GetLength(0); i++)
            {
                for (int j = 0; j < ret.GetLength(1); j++)
                {
                    sum += matrix[i, j];
                }
            }

            if (Math.Abs(sum) > 0)
            {
                for (int i = 0; i < ret.GetLength(0); i++)
                {
                    for (int j = 0; j < ret.GetLength(1); j++)
                    {
                        ret[i, j] = matrix[i, j] / sum;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// GaussianConvolution
        /// </summary>
        /// <param name="matrix">matrix</param>
        /// <param name="deviation">deviation</param>
        /// <returns>double</returns>
        public static double[,] GaussianConvolution(double[,] matrix, double deviation)
        {
            var kernel = CalculateNormalized1DSampleKernel(deviation);
            var res1 = new double[matrix.GetLength(0), matrix.GetLength(1)];
            var res2 = new double[matrix.GetLength(0), matrix.GetLength(1)];

            //x-direction
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    res1[i, j] = ProcessPoint(matrix, i, j, kernel, 0);
                }
            }

            //y-direction
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    res2[i, j] = ProcessPoint(res1, i, j, kernel, 1);
                }
            }

            return res2;
        }

        /// <summary>
        /// 对图片进行高斯模糊
        /// </summary>
        /// <param name="d">模糊数值，数值越大模糊越很</param>
        /// <param name="image">一个需要处理的图片</param>
        /// <returns>图片</returns>
        public Bitmap FilterProcessImage(double d, Bitmap image)
        {
            var ret = new Bitmap(image.Width, image.Height);
            var matrixR = new double[image.Width, image.Height];
            var matrixG = new double[image.Width, image.Height];
            var matrixB = new double[image.Width, image.Height];
            for (var i = 0; i < image.Width; i++)
            {
                for (var j = 0; j < image.Height; j++)
                {
                    //matrix[i, j] = GrayScale(image.GetPixel(i, j)).R;
                    matrixR[i, j] = image.GetPixel(i, j).R;
                    matrixG[i, j] = image.GetPixel(i, j).G;
                    matrixB[i, j] = image.GetPixel(i, j).B;
                }
            }

            matrixR = GaussianConvolution(matrixR, d);
            matrixG = GaussianConvolution(matrixG, d);
            matrixB = GaussianConvolution(matrixB, d);
            for (var i = 0; i < image.Width; i++)
            {
                for (var j = 0; j < image.Height; j++)
                {
                    var r = (int)Math.Min(255, matrixR[i, j]);
                    var g = (int)Math.Min(255, matrixG[i, j]);
                    var b = (int)Math.Min(255, matrixB[i, j]);
                    ret.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            return ret;
        }

        private static double ProcessPoint(double[,] matrix, int x, int y, double[,] kernel, int direction)
        {
            double res = 0;
            var half = kernel.GetLength(0) / 2;
            for (var i = 0; i < kernel.GetLength(0); i++)
            {
                var cox = direction == 0 ? x + i - half : x;
                var coy = direction == 1 ? y + i - half : y;
                if (cox >= 0 && cox < matrix.GetLength(0) && coy >= 0 && coy < matrix.GetLength(1))
                {
                    res += matrix[cox, coy] * kernel[i, 0];
                }
            }

            return res;
        }
    }

    #endregion 高斯模糊算法
}