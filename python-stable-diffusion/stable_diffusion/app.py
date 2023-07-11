import os
from diffusers import StableDiffusionPipeline
import torch
from fastapi import FastAPI, Query
from starlette.responses import StreamingResponse
import io

SDV5_MODEL_PATH = os.getenv('SDV5_MODEL_PATH')

app = FastAPI()

pipe = StableDiffusionPipeline.from_pretrained(SDV5_MODEL_PATH, torch_dtype=torch.float16, revision="fp16")
pipe = pipe.to("cuda")
torch.inference_mode()
pipe.enable_attention_slicing()

@app.get("/")
async def root():
    return {"message": "Welcome to Stable Diffusion API!"}

@app.get("/generate", response_class=StreamingResponse)
async def generate(prompt: str = Query(..., min_length=1, max_length=200, description="Prompt to generate image from")):

    image = pipe(prompt).images[0]
    image_bytes = io.BytesIO()
    image.save(image_bytes, format='PNG')
    image_bytes.seek(0)

    return StreamingResponse(image_bytes, media_type="image/png", headers={"Content-Disposition": "attachment; filename=image.png"}, status_code=200)
