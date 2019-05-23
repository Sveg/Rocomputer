import socket
import time
from sense_hat import SenseHat, ACTION_RELEASED, ACTION_HELD, ACTION_PRESSED
sense = SenseHat()
UDP_Port = 1111
from datetime import datetime
g = [0, 255, 0]
y = [255, 180, 0]
r = [255, 0, 0]
w = [255, 255, 255]
sense.set_imu_config(True, True, True)
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
def time1(event):
	if event.action != ACTION_RELEASED:
		while True:
			dt = timestamp2 - timestamp
			msg = str(dt)
			sense.show_message(msg,scroll_speed = 0.1) 

def stop(event):
	if event.action != ACTION_RELEASED:
		data2 = "stop"
		sock.sendto(bytes(data2,"UTF-8"),('<broadcast>',UDP_Port))
		
def compass(event):
	if event.action != ACTION_PRESSED:
		compasss = sense.get_compass()
		if compasss < 45 or compasss > 315:
			sense.show_letter('N')
			sense.clear()
		elif compasss < 135:
			sense.show_letter('E')
			sense.clear()
		elif compasss < 225:
			sense.show_letter('S')
			sense.clear()
		else:
			sense.show_letter('W')
			sense.clear()


def sendData(event):
	if event.action != ACTION_PRESSED:
		while True:
			accel = sense.get_accelerometer_raw()
			x = accel['x']
			x = round(x,5)
			data = "Current time " + str(datetime.now()) + " Force of a stroke: " + str(x)
			sock.sendto(bytes(data,"UTF-8"),('<broadcast>',UDP_Port))
			if x >= 0:
				print(data)
			time.sleep(0.5)
			if x >= 1:
				sense.set_pixels(greenpixel)
			else:
				sense.clear()
				break
			sense.stick.direction_right = stop
			sense.stick.direction_left = temperatur
			sense.stick.direction_down = compass
def temperatur(event):
	if event.action != ACTION_PRESSED:
		t = sense.get_temperature()
		t = round(t,1)
		message = str(t)
		sense.show_message(message, scroll_speed=0.1)

while True:
	sense.stick.direction_middle = sendData	
	sense.stick.direction_right = stop
	sense.stick.direction_left = temperatur
	sense.stick.direction_down = compass
