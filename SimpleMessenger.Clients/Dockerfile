FROM nginx:alpine
RUN rm /etc/nginx/nginx.conf

COPY nginx.conf /etc/nginx/nginx.conf

COPY HistoryClient /usr/share/nginx/html/history_client/
COPY ReceiverClient /usr/share/nginx/html/receiver_client/
COPY SenderClient /usr/share/nginx/html/sender_client/

EXPOSE 80

CMD [ "nginx", "-g", "daemon off;" ]