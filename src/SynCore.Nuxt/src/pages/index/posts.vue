<script setup lang="ts">

interface Post {
  id: string
  content: string,
  user: {
    id: string,
    name: string,
  },
  nLikes: number,
  nComments: number
}

const { getAllPosts, addPost } = useApiClient()

const content = ref('')
const posts = ref<Post[]>([])

async function sendPost() {
  try {
    const res = await addPost({ content: content.value })
    const newPost = res.data as Post

    posts.value.push(newPost)

    content.value = ''
  } catch (err) {
    console.log(err)
  }
}

onMounted(async () => {
  try {
    const res = await getAllPosts()
    posts.value = res.data as Post[]
  } catch (err) {
    console.error(err)
  }
})
</script>

<template>
  <div class="flex justify-content-center pt-4">
    <div style="width: 80%; max-width: 1000px;" class="flex flex-column gap-4">
      <Card class="shadow-none border-1 border-gray-200">
        <template #title>Crie um post</template>
        <template #content>
          <form class="flex flex-column gap-3">
            <Textarea placeholder="Digite algo..." class="bg-gray-100 w-full" v-model="content" ></Textarea>
            <Button class="align-self-end" label="Enviar" icon="pi pi-send" @click="sendPost"></Button>
          </form>
        </template>
      </Card>

      <Card v-for="post in posts" :key="post.id" class="shadow-none border-1 border-gray-200">
        <template #title>
          <div>
            <span class="pi pi-user"></span>
            <span style="font-size: 16px" class="ml-2">{{post.user.name}}</span>
          </div>
        </template>
        <template #content>
          <div class="flex flex-column gap-3">
            <div style="max-height: 300px" class="w-full bg-gray-100 p-3 border-round overflow-auto">
              {{post.content}}
            </div>
            <div>
              <Button icon="pi pi-heart" text :label="`${post.nLikes} Likes`" severity="danger" :pt="{ label: 'custom-label-like-btn' }" />
              <Button icon="pi pi-bars" text :label="`${post.nComments} Comentários`" severity="info" />
            </div>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<style>
.custom-label-like-btn {
  color: #64748b;
}
</style>