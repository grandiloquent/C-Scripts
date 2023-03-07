from PIL import Image
im1 = Image.open('C:/Users/Administrator/Desktop/20230307/001/1/000020.png')
im11= im1.crop((327, 92, 765, 708))
im2 = Image.open('C:/Users/Administrator/Desktop/20230307/001/1/000021.png')
im22= im2.crop((327, 92, 765, 708))

def merge(im1, im2):
    w = im1.size[0] + im2.size[0]
    h = max(im1.size[1], im2.size[1])
    im = Image.new("RGBA", (w, h))

    im.paste(im1)
    im.paste(im2, (im1.size[0], 0))

    return im


im3 = merge(im11, im22)
im3.save('C:/Users/Administrator/Desktop/20230307/001/1/21.png')

# C:\Users\Administrator\AppData\Local\Programs\Python\Python310\python.exe 001.py
