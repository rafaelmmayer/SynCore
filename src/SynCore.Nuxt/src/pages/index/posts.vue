<script setup lang="ts">

interface Post {
  id: string
  content: string,
  user: {
    id: string,
    name: string,
  },
  nLikes: number,
  nComments: number,
  commentInput: string,
  comments: { id: string, comment: string, userId?: string, userName?: string }[],
  isCommentsVisible: boolean
}

const { getAllPosts, addPost, addLike, deletePost } = useApiClient()
const authUser = useAuthUser()

const content = ref('')
const posts = ref<Post[]>([])
const reportDialogVisible = ref(false)

async function sendPost() {
  try {
    const res = await addPost({ content: content.value })
    const newPost = res.data as Post

    newPost.comments = []

    posts.value.push(newPost)

    content.value = ''
  } catch (err) {
    console.log(err)
  }
}

async function giveLike(post: Post) {
  try {
    const res = await addLike(post.id)
    post.nLikes++
  } catch (err) {
    console.log(err)
  }
}

async function removePost(post: Post) {
  try {
    const res = await deletePost(post.id)
    const i = posts.value.indexOf(post)
    posts.value.splice(i, 1)
  } catch (err) {
    console.log(err)
  }
}

async function handleCommentsButton(post: Post) {
  post.isCommentsVisible = !post.isCommentsVisible
}

async function addComment(post: Post) {
  const comment = {
    id: '',
    comment: post.commentInput,
    userId: authUser.user?.id,
    userName: authUser.user?.name,
  }

  post.comments.push(comment)
  post.commentInput = ''
  post.nComments++
}

onMounted(async () => {
  try {
    const res = await getAllPosts()
    posts.value = res.data as Post[]
    posts.value.forEach(post => {
      post.comments = []
    })
  } catch (err) {
    console.error(err)
  }
})
</script>

<template>
  <div class="flex justify-content-center pt-4 h-full overflow-auto">
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
          <div class="flex align-items-center justify-content-between">
            <div>
              <span class="pi pi-user"></span>
              <span style="font-size: 16px" class="ml-2">{{post.user.name}}</span>
            </div>
            <Button
                v-if="authUser.user?.id === post.user.id"
                icon="pi pi-times"
                text
                rounded
                size="small"
                severity="danger"
                :pt="{ root: 'custom-small-button' }"
                @click="removePost(post)"
            />
          </div>
        </template>
        <template #content>
          <div class="flex flex-column gap-3">
            <div style="max-height: 300px" class="w-full bg-gray-100 p-3 border-round overflow-auto">
              {{post.content}}
            </div>
            <div class="flex align-items-center">
              <Button icon="pi pi-heart" text :label="`${post.nLikes} Likes`" severity="danger" :pt="{ label: 'custom-label-like-btn' }" @click="giveLike(post)" />
              <div class="flex-grow-1">
                <Button icon="pi pi-bars" text :label="`${post.nComments} Comentários`" severity="info" @click="handleCommentsButton(post)" />
              </div>
              <Button
                  icon="pi pi-megaphone"
                  text
                  rounded
                  size="small"
                  severity="danger"
                  :pt="{ root: 'custom-small-button' }"
                  @click="reportDialogVisible = true"
              />
            </div>
            <div v-if="post.isCommentsVisible">
              <div class="flex align-items-center gap-2">
                <InputText class="w-full" type="text" v-model="post.commentInput" placeholder="Comente algo..." />
                <Button
                    icon="pi pi-send"
                    text
                    rounded
                    size="small"
                    :pt="{ root: 'custom-small-button' }"
                    @click="addComment(post)"
                />
              </div>
              <div v-for="comment in post.comments" :key="comment.id" class="mt-4">
                <span class="font-bold">{{comment.userName}}:</span>
                <span class="ml-2">{{comment.comment}}</span>
              </div>
            </div>
          </div>
        </template>
      </Card>
    </div>
  </div>

  <Dialog v-model:visible="reportDialogVisible" modal header="Denuncie" :style="{ width: '25rem' }">
    <span class="p-text-secondary block mb-5">Digite o motivo para a denúncia. Um e-mail será enviado para o suporte analisar.</span>
    <div class="flex align-items-center gap-3 mb-5">
      <Textarea id="report" class="flex-auto" autocomplete="off" />
    </div>
    <div class="flex justify-content-end gap-2">
      <Button type="button" label="Cancelar" severity="secondary" @click="reportDialogVisible = false"></Button>
      <Button type="button" label="Enviar" @click="reportDialogVisible = false"></Button>
    </div>
  </Dialog>
</template>

<style>
.custom-label-like-btn {
  color: #64748b;
}
</style>