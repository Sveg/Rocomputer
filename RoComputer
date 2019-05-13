import socket
import time
from sense_hat import SenseHat, ACTION_RELEASED, ACTION_HELD, ACTION_PRESSED
sense = SenseHat()
UDP_Port = 1111
from datetime import datetime
g = [0, 255, 0]
y = [255, 180, 0]
r = [255, 0, 0]
sense.set_imu_config(False, False, True)
redpixel = [
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r,
	r, r, r, r, r, r, r, r
]

yellowpixel = [
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y,
	y, y, y, y, y, y, y, y
]

greenpixel = [
	g, g, g, g, g, g, g, g,
	g, g, g, g, g, g, g, g,
	g, g, g, g, g, g, g, g,
	g, g, g, g, g, g, g, g,
	g, g, g, g, g, g, g, g,
	g, g, g, g, g, g, g, g, 
	g, g, g, g, g, g, g, g,
	g, g, g, g, g, g, g, g
]
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
def stop(event):
	if event.action != ACTION_RELEASED:
		data2 = "stop"
		sock.sendto(bytes(data2,"UTF-8"),('<broadcast>',UDP_Port))


def sendData(event):
	if event.action != ACTION_PRESSED:
		amount = 0
		while True:
			accel = sense.get_accelerometer_raw()
			x = accel['x']
			x = round(x,5)
			data = "Current time " + str(datetime.now()) + " Force of a stroke: " + str(x)
			sock.sendto(bytes(data,"UTF-8"),('<broadcast>',UDP_Port))
			if x >= 0:
				print(data)
			amount = amount + 1
			time.sleep(0.5)
			if x <= 0.30:
				sense.set_pixels(redpixel)
			if x >= 0.32 or x < 0.6:
				sense.set_pixels(yellowpixel)
			else:
				sense.clear()
			if x >= 1.65:
				sense.set_pixels(greenpixel)
			else:
				sense.clear()
			#if amount == 10000:
				break
			sense.stick.direction_right = stop
while True:
	sense.stick.direction_middle = sendData
	#lol	
