helm upgrade mintax-test-etax -f mintax-test-etax/values.yaml ./mintax-test-etax
helm upgrade mintax-test-amisconnector -f mintax-test-amis-connector/values.yaml ./mintax-test-amis-connector
helm upgrade mintax-test-web -f mintax-ui/values.yaml ./mintax-ui
helm upgrade mintax-test-general -f mintax-test-general/values.yaml ./mintax-test-general
helm upgrade mintax-auth -f mintax-auth/values.yaml ./mintax-auth
helm upgrade mintax-gateway -f mintax-gateway-nginx/values.yaml ./mintax-gateway-nginx
helm upgrade mintax-mtaxconnector -f mintax-test-mtax-connector/values.yaml ./mintax-test-mtax-connector
helm upgrade mintax-worker -f mintax-test-worker/values.yaml ./mintax-test-worker
helm upgrade mintax-test-import -f mintax-test-import/values.yaml ./mintax-test-import
kubectl delete --all pods --namespace=mintax-test
