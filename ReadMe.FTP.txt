﻿* Install FTP server on Ubuntu:

sudo apt-get update
sudo apt-get install vsftpd

# Edit file /etc/vsftpd.conf:
anonymous_enable=NO
connect_from_port_20=YES
dirmessage_enable=YES
force_local_data_ssl=YES
force_local_logins_ssl=YES
listen_ipv6=YES
listen=NO
local_enable=YES

userlist_deny=NO
userlist_enable=YES
userlist_file=/etc/vsftpd.user_list

write_enable=YES
local_umask=022
pam_service_name=vsftpd
pasv_enable=Yes
pasv_max_port=10200
pasv_min_port=10100
rsa_cert_file=/etc/ssl/private/vsftpd.pem
rsa_private_key_file=/etc/ssl/private/vsftpd.pem
secure_chroot_dir=/var/run/vsftpd/empty
ssl_ciphers=HIGH
ssl_enable=YES		#> Maybe change YES -> NO
ssl_sslv2=NO
ssl_sslv3=NO
ssl_tlsv1=YES
use_localtime=YES
xferlog_enable=YES


# Allow fireware cross:

sudo ufw allow from any to any port 20,21,10000:10100 proto tcp

sudo ufw allow 6379/tcp
sudo ufw allow 20/tcp
sudo ufw allow 21/tcp
sudo ufw allow 990/tcp
sudo ufw allow 40000:50000/tcp
sudo ufw status

# Create UserName and Password:

sudo mkdir /home/ftpuser

sudo useradd -m ftpuser
sudo passwd ftpuser

Enter new UNIX password:	123456
Retype new UNIX password:	123456
passwd: password updated successfully 


sudo bash -c "echo FTP TESTING > /home/ftpuser"


# Create the FTP folder, add user and assign a directory to it.

sudo adduser --home=/home/ftpuser ftpuser
sudo chmod 777 /home/ftpuser

# Set the ownership, and make sure to remove write permissions with the following commands :

sudo chown nobody:nogroup /home/ftpuser
sudo chmod a-w /home/ftpuser

sudo chown -R root /usr/share/npm/node_modules

sudo chown -R root /home/ftpuser

# Let’s verify the permissions:

sudo ls -la /home/ftpuser

-----------------------------------------------
-----------------------------------------------

# Show status:
systemctl status -l vsftpd.service

# vsftpd needs the /etc/vsftpd.conf to be owned by root. Check who owns it with
ls -la /etc

# To change the owner run the following command as as root or via sudo
chown root /etc/vsftpd.conf

# Then regenerate SSL certificate (or reissue from your provider):
sudo openssl req -x509 -nodes -days 1095 -newkey rsa:2048 -keyout /etc/ssl/private/vsftpd.pem -out /etc/ssl/private/vsftpd.pem

Output
Generating a 2048 bit RSA private key
............................................................................+++
...........+++
writing new private key to '/etc/ssl/private/vsftpd.pem'
-----
You are about to be asked to enter information that will be incorporated
into your certificate request.
What you are about to enter is what is called a Distinguished Name or a DN.
There are quite a few fields but you can leave some blank
For some fields there will be a default value,
If you enter '.', the field will be left blank.
-----
Country Name (2 letter code) [AU]:US
State or Province Name (full name) [Some-State]:NY
Locality Name (eg, city) []:New York City
Organization Name (eg, company) [Internet Widgits Pty Ltd]:DigitalOcean
Organizational Unit Name (eg, section) []:
Common Name (e.g. server FQDN or YOUR name) []: your_IP_address
Email Address []:

-----------------------------------------------

lsb_release -d

sudo lsof -i -P -n | grep LISTEN
netstat -tanp
lsof -i | grep ftp
killall -i redis-ser
kill PID

service vsftpd status
sudo journalctl | grep -i vsftp


/etc/init.d/vsftpd restart
sudo systemctl restart vsftpd
sudo systemctl restart vsftpd.service


nano /etc/vsftpd.conf
nano /etc/vsftpd.user_list

Test local: 10.103.1.150 administrator - 123456
ftpuser 123456

sudo bash -c "echo FTP TESTING > /home/ftp"

ftp 10.103.1.150

http://10.103.1.150:8080

sudo mv /etc/vsftpd.conf /home/vsftpd.conf_bak

find / -xdev 2>/dev/null -name "node_modules"