test:
	skaffold build -d misadockerhub.vn:30100 -f skaffold-test.yaml
	skaffold build -q -d misadockerhub.vn:30100 -f skaffold-test.yaml | tee v1.5.json | skaffold deploy -f skaffold-test.yaml --build-artifacts -
dev:
	skaffold build -d misadockerhub.vn:30100 -f skaffold-dev.yaml
	skaffold build -q -d misadockerhub.vn:30100 -f skaffold-dev.yaml | tee v1.5.json | skaffold deploy -f skaffold-dev.yaml --build-artifacts -
demo:
	skaffold build -d misadockerhub.vn:30100 -f skaffold-demo.yaml
	skaffold build -q -d misadockerhub.vn:30100 -f skaffold-demo.yaml | tee v1.5.json | skaffold deploy -f skaffold-demo.yaml --build-artifacts -
