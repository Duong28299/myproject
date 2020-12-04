helm upgrade mintax-etax -f mintax-etax/values.yaml ./mintax-etax
helm upgrade mintax-web -f mintax-ui/values.yaml ./mintax-ui
helm upgrade mintax-gateway -f mintax-gateway-nginx/values.yaml ./mintax-gateway-ngnix
helm upgrade mintax-amisconnector -f mintax-amis-connector/values.yaml ./mintax-amis-connector
helm upgrade mintax-general -f mintax-general/values.yaml ./mintax-general
helm upgrade mintax-mtaxconnector -f mintax-mtax-connector/values.yaml ./mintax-mtax-connector
kubectl delete --all pod --namespace=mintax-dev

